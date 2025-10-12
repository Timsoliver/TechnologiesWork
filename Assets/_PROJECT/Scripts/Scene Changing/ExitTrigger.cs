using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
   [SerializeField] private string mainMenuSceneName = "MainMenu";

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         SceneManager.LoadScene(mainMenuSceneName);
      }
   }
}
