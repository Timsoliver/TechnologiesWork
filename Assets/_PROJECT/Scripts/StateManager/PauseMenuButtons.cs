using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    public void TogglePause()
    {
        GameState current = GameStateManager.Instance.CurrentGameState;
        GameState newState =  current == GameState.Gameplay
            ? GameState.Paused
            : GameState.Gameplay;
        
        GameStateManager.Instance.SetState(newState);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
