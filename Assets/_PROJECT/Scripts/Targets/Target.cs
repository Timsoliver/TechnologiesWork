using UnityEngine;

public class Target : MonoBehaviour
{
  public float health = 10f;

  void OnEnable()
  {
    health = 10f;
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
    health = 10f;
  }
}
