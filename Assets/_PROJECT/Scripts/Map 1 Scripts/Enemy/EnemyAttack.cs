using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject activeUI;

    private void Start()
    {
        activeUI = GameObject.Find("Active UI");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            if (activeUI != null)
                activeUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            if (activeUI != null)
                activeUI.SetActive(false);
        }
    }
}
