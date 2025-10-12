using UnityEngine;
using System.Collections;


public class Spawner : MonoBehaviour
{
    [Header("Pool and Spawning")]
    [SerializeField] private ObjectPool objectPool;

    [SerializeField] private int enableOnActivateCount = 5;
    [SerializeField] private bool autoSpawn = false;
    [SerializeField] private int maxActiveTargets = 3;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    
    [Header("Optional")]
    public bool callHighScoreOnDisable = false;
   
    private Coroutine spawnCoroutine;

    private void Reset()
    {
        if (objectPool == null)
            objectPool = FindObjectOfType<ObjectPool>();
    }

    private void OnEnable()
    {
        if (objectPool == null)
        {
            objectPool = FindObjectOfType<ObjectPool>();
            if (objectPool == null)
            {
                Debug.LogError("Spawner: No ObjectPool found.");
                return;
            }
        }

        for (int i = 0; i < enableOnActivateCount; i++)
        {
            TryActivateOne();
        }

        if (autoSpawn)
            spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        
        if (objectPool != null)
            objectPool.ReturnAll();
        
        if (callHighScoreOnDisable && GameManager.Instance != null)
            GameManager.Instance.CheckHighScoreAndReset();
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int activeCount = objectPool.GetAllActive().Length;
            if (activeCount < maxActiveTargets)
                TryActivateOne();
        }
    }

    private void TryActivateOne()
    {
        GameObject t = objectPool.GetTarget();
        if (t != null)
        {
            t.transform.position = GetRandomPosition();
            t.SetActive(true);
        }
    }
    
    Vector3 GetRandomPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        
        return transform.position + randomOffset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
