using UnityEngine;

public class Target : MonoBehaviour
{
  [SerializeField] private float startHealth = 10f;
  private float health;

  void OnEnable()
  {
    health = startHealth;
  }
  public void TakeDamage(float amount)
  {
    health -= amount;
    if (health <= 0)
    {
      Die();
    }
  }

  void Die()
  {
    if(GameManager.Instance != null)
      GameManager.Instance.AddScore(1);
    
    gameObject.SetActive(false);
  }
}
