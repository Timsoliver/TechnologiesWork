using UnityEngine;
using System.Collections.Generic;

public class EnemyObjPool : MonoBehaviour
{
  [Header("Settings")] 
  [SerializeField] private GameObject enemyPrefab;
  [SerializeField] private int poolSize = 10;
  
  private List<GameObject> pool = new List<GameObject>();

  private void Awake()
  {
    for (int i = 0; i < poolSize; i++)
    {
      GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, transform);
      enemy.SetActive(false);
      pool.Add(enemy);
    }
  }

  public GameObject GetPooledEnemy()
  {
    for (int i = 0; i < poolSize; i++)
    {
      if (!pool[i].activeInHierarchy)
      {
        return pool[i];
      }
    }
    return null;
  }

  public void ReturToPool(GameObject enemy)
  {
    enemy.SetActive(false);
    enemy.transform.SetParent(transform);
  }
}
