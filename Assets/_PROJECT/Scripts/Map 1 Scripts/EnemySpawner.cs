using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private EnemyObjPool enemyObjPool;

    [Header("Settings")] 
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnHeightOffset = 0.5f;
    
    private List<Vector3> spawnPoints = new List<Vector3>();
    private int nextSpawnPoint = 0;

    private void Start()
    {
        StartCoroutine(SpawningRoutine());
    }

    private IEnumerator SpawningRoutine()
    {
        yield return new WaitUntil(() => mazeGenerator!= null && mazeGenerator.MazeGenerated);
        
        yield return new WaitUntil(() => GameObject.FindGameObjectWithTag("Player") != null);

        SetupSpawnPoints();

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnAt(spawnPoints[i]);
        }

        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            
            Vector3 position = spawnPoints[nextSpawnPoint];
            SpawnAt(position);

            nextSpawnPoint++;
            if (nextSpawnPoint == spawnPoints.Count)
            {
                nextSpawnPoint = 0;
            }
        }
    }
    
    private void SetupSpawnPoints()
    {
        spawnPoints.Clear();

        int width = mazeGenerator.MazeWidth;
        int depth = mazeGenerator.MazeDepth;
            
        Vector3 spawn1 = mazeGenerator.GetCellPos(0, depth -1);
        Vector3 spawn2 = mazeGenerator.GetCellPos( 0,  0);
        Vector3 spawn3 = mazeGenerator.GetCellPos(width -1, 0);
            
        spawn1 += Vector3.up * spawnHeightOffset;
        spawn2 += Vector3.up * spawnHeightOffset;
        spawn3 += Vector3.up * spawnHeightOffset;
            
        spawnPoints.Add(spawn1);
        spawnPoints.Add(spawn2);
        spawnPoints.Add(spawn3);
    }

    private void SpawnAt(Vector3 position)
    {
        GameObject enemy = enemyObjPool.GetPooledEnemy();
        if (enemy == null)
        {
            return;
        }
        
        enemy.transform.position = position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.transform.SetParent(null);
        enemy.SetActive(true);
    }
}
