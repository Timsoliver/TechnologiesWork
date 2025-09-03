using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int poolSize = 5;
    [SerializeField] private int maxActiveTargets = 3;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    [SerializeField] private GameObject targetPrefab;
    
    private ObjectPool objectPool;
    private float spawnTimer;
    private List<GameObject> targetPool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject t = Instantiate(targetPrefab);
            t.SetActive(true);
            t.transform.position = GetRandomPosition();
            targetPool.Add(t);
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnTarget();
        }
    }

    void TrySpawnTarget()
    {
        int activeCount = 0;
        foreach (GameObject t in targetPool)
        {
            if (t.activeInHierarchy) activeCount++;
        }

        if (activeCount < maxActiveTargets) return;

        foreach (GameObject t in targetPool)
        {
            if (!t.activeInHierarchy)
            {
                t.transform.position = GetRandomPosition();
                t.SetActive(true);
                break;
            }
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
