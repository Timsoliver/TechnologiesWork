using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [Header("Pooling Settings")]
  [SerializeField] private GameObject targetPrefab;
  [SerializeField] private int poolStartSize = 5;
  
  [SerializeField]
  private Queue<GameObject> inactivePool = new Queue<GameObject>();
  private HashSet<GameObject> activeTargets = new HashSet<GameObject>();
  

  private void Start()
  {
    for (int i = 0; i < poolStartSize; i++)
    {
      GameObject t = Instantiate(targetPrefab);
      t.SetActive(false);
      inactivePool.Enqueue(t);
    }
  }

  public GameObject GetTarget()
  {
    GameObject target;
    
    if (inactivePool.Count > 0)
    {
      target = inactivePool.Dequeue();
    }
    else
    {
      target = Instantiate(targetPrefab);
    }

    if (!activeTargets.Contains(target))
      activeTargets.Add(target);
    
    target.SetActive(true);
    return target;
  }

  public void ReturnTarget(GameObject target)
  {
    if (target == null) return;
    
    if (activeTargets.Contains(target))
      activeTargets.Remove(target);
    
    target.SetActive(false);
    
    if (!inactivePool.Contains(target))
      inactivePool.Enqueue(target);
  }

  public void ReturnAll()
  {
    GameObject[] activeArray = new GameObject [activeTargets.Count];
    activeTargets.CopyTo(activeArray);

    foreach (var t in activeArray)
    {
      ReturnTarget(t);
    }
  }

  public GameObject[] GetAllActive()
  {
    GameObject[] arr = new GameObject[activeTargets.Count];
    activeTargets.CopyTo(arr);
    return arr;
  }
}
