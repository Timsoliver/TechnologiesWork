using UnityEngine;

public class Portal : MonoBehaviour
{
   [SerializeField] Transform destination;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         if (other.TryGetComponent<Movement>(out var movement))
         {
            movement.Teleport(destination.position, destination.rotation);
         }
      }
   }

   void OnDrawGizmos()
   {
      if (destination == null) return;
      
      Gizmos.color = Color.white;
      Gizmos.DrawWireSphere(destination.position, 1f);
      Gizmos.DrawLine(destination.position, destination.position + destination.forward * 2f);
   }
}
