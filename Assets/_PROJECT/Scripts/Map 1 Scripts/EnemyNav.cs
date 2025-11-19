using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    private NavMeshAgent enemy;
    private Transform player;
    
    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        if (enemy != null)
        {
            enemy.isStopped = false;
            enemy.ResetPath();
        }

        FindPlayer();
    }
  
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
        
        enemy.SetDestination(player.position);
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
}
