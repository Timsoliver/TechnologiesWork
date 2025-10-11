using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public ButtonData buttonData;

    private Material mat;
    private Color originalColor;
    private bool isChanged = false;
    
    public GameObject targetObject;

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

        if (!string.IsNullOrEmpty(buttonData.targetObjectName))
        {
            targetObject = FindInactiveByName(buttonData.targetObjectName);
            if (targetObject == null)
            {
                Debug.LogWarning($"[{name}] Could not find TargetObject : {buttonData.targetObjectName}");
            }
        }
    }

    public string GetDescription()
    {
        if (targetObject != null)
        {
            return targetObject.activeSelf ? buttonData.onDescription : buttonData.offDescription;
        }
        
        return string.IsNullOrEmpty(buttonData.description) ? "Interact" : buttonData.description;
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name} interacted with using {buttonData.name}");

        if (mat == null) return;

        mat.color = isChanged ? originalColor : buttonData.chosenColor;
        isChanged = !isChanged;
        
        if (targetObject != null)
        {
            bool newState = !targetObject.activeSelf;
            targetObject.SetActive(newState);
            Debug.Log($"{targetObject.name} active: {newState}");
        }
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
}
