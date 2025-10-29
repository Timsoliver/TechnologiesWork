using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] Vector3 desiredDestination;
    void Start()
    {
        GetComponent<NavMeshAgent>().destination = desiredDestination;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
