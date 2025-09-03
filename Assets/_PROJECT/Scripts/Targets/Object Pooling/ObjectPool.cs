using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [SerializeField]
  private GameObject targetPrefab;
  
  [SerializeField]
  private Queue<GameObject> targetPool = new Queue<GameObject>();

  [SerializeField] private int poolStartSize = 3;

  private void Start()
  {
    for (int i = 0; i < poolStartSize; i++)
    {
      GameObject target = Instantiate(targetPrefab);
      targetPool.Enqueue(target);
      target.SetActive(false);
    }
  }

  public GameObject GetTarget()
  {
    if (targetPool.Count > 0)
    {
      GameObject target = targetPool.Dequeue();
      target.SetActive(true);
      return target;
    }
    else
    {
      GameObject target = Instantiate(targetPrefab);
      return target;
    }
  }

  public void ReturnTarget(GameObject target)
  {
    targetPool.Enqueue(target);
    target.SetActive(false);
  }
}
