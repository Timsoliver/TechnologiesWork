using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Aim Trainer");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();
    }
}