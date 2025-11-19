using UnityEngine;

public class TargetReturn : MonoBehaviour
{
   private ObjectPool objectPool;

   private void Start()
   {
      objectPool = FindObjectOfType<ObjectPool>();
   }

   private void OnDisable()
   {
      if(objectPool != null)
         objectPool.ReturnTarget(this.gameObject);
   }
}
