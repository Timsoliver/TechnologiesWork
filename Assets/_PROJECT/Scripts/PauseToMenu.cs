using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseToMenu : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "Main Menu";

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}