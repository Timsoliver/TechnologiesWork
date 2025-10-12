using UnityEngine;

public class PauseUIController : MonoBehaviour
{
   [SerializeField] private GameObject gameplayUI;
   [SerializeField] private GameObject pauseMenuUI;

   private void Awake()
   {
      GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
   }

   private void OnDestroy()
   {
      GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
   }

   private void OnGameStateChanged(GameState newGamestate)
   {
      bool isPaused = newGamestate == GameState.Paused;
      
      if (gameplayUI != null)
         gameplayUI.SetActive(!isPaused);
      if(pauseMenuUI != null)
         pauseMenuUI.SetActive(isPaused);
      
      Cursor.visible = isPaused;
      Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
   }
}
