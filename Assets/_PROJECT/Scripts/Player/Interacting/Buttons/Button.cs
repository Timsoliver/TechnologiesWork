using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Button : MonoBehaviour, IInteractable
{
    public ButtonData buttonData;

    
    private Material mat;
    private Color originalColor;
    private bool isChanged = false;
    
    private GameObject targetObjectA;
    private GameObject targetObjectB;
    
    [Header("Targets to control on Enable/Disable")]
    public List<GameObject> targetObjects = new List<GameObject>();
    
    [Header("Timer Settings")]
    public bool useTimer = false;
    public float timerDuration = 60f;
    private float timerRemaining;
    private Coroutine timerCoroutine;
    
    [SerializeField] private TMP_Text timerText;

    private Dictionary<GameObject, bool> previousTargetStates = new Dictionary<GameObject, bool>();

    private void Start()
    {
        if (buttonData == null)
        {
            Debug.LogError($"{name} has no ButtonData");
            enabled = false;
            return;
        }

        mat = GetComponent<MeshRenderer>().material;
        originalColor = mat.color;

        targetObjectA = FindInactiveByName(buttonData.targetObjectNameA);
        targetObjectB = FindInactiveByName(buttonData.targetObjectNameB);
        
        if (targetObjectA == null)
            Debug.LogWarning($"[{name}] Could not find target A: {buttonData.targetObjectNameA}");
        if (targetObjectB == null) 
            Debug.LogWarning($"[{name}] Could not find target B: {buttonData.targetObjectNameB}");

        if (timerText != null)
            timerText.text = "";
    }

    private void OnEnable()
    {
        foreach (var t in targetObjects)
        {
            if (t != null)
                t.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var t in targetObjects)
        {
            if (t != null)
                t.SetActive(false);
        }

        if (GameManager.Instance != null)
            GameManager.Instance.CheckHighScoreAndReset();
    }
    public string GetDescription()
    {
        if (targetObjectA != null)
        {
            return targetObjectA.activeSelf ? buttonData.onDescription : buttonData.offDescription;
        }
        return string.IsNullOrEmpty(buttonData.description) ? "Interact" : buttonData.description;
    }

    public void Interact()
    {
        previousTargetStates.Clear();
        if (targetObjectA != null)
            previousTargetStates[targetObjectA] = targetObjectA.activeSelf;
        if (targetObjectB != null)
            previousTargetStates[targetObjectB] = targetObjectB.activeSelf;
        
        bool newState = targetObjectA != null ? !targetObjectA.activeSelf : false;
        if (targetObjectA != null) targetObjectA.SetActive(newState);
        if(targetObjectB != null) targetObjectB.SetActive(!newState);
        
        mat.color = isChanged ? originalColor : buttonData.chosenColor;
        isChanged = !isChanged;
        
        if (useTimer)
            StartTimer();
    }

    private GameObject FindInactiveByName(string name)
    {
        foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.name == name)
                return obj;
        }
        return null;
    }

    private void StartTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        timerRemaining = timerDuration;

        while (timerRemaining > 0)
        {
            timerRemaining -= Time.deltaTime;
            UpdateTimerText(timerRemaining);
            yield return null;
        }
        
        ResetButtonColor();

        foreach (var kvp in previousTargetStates)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }
        
        UpdateTimerText(0);
    }

    private void ResetButtonColor()
    {
        mat.color = originalColor;
        isChanged = false; 
    }

    private void UpdateTimerText(float time)
    {
        if (timerText == null) return;

        if (time>0)
        {
            int seconds = Mathf.CeilToInt(time);
            timerText.text = seconds.ToString();
        }
        else
        {
            timerText.text = "";
        }
    }

    public void ResetTimer()
    {
        if (useTimer)
        {
            if (timerCoroutine != null)
                StopCoroutine(timerCoroutine);
            
            timerRemaining = timerDuration;
            ResetButtonColor();
            UpdateTimerText(0);
        }
    }
}
