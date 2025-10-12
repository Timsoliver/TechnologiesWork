using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
   private PlayerControls controls;

   private void Awake()
   {
      controls = new PlayerControls();
      controls.UI.Pause.performed += OnPausePressed;
   }

   private void OnEnable()
   {
      controls.UI.Enable();
   }

   private void OnDisable()
   {
      controls.UI.Disable();
   }

   private void OnPausePressed(InputAction.CallbackContext context)
   {
      GameState currentGameState = GameStateManager.Instance.CurrentGameState;
      GameState newGameState = currentGameState == GameState.Gameplay
         ? GameState.Paused
         : GameState.Gameplay;

      GameStateManager.Instance.SetState(newGameState);
   }
}