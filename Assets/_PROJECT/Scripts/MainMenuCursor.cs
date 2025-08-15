using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    void Start()
    {
        // Make the cursor visible and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}