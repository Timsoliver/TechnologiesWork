using UnityEngine;

public class OrbMenuController : MonoBehaviour
{
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject orbMenuUI;
    [SerializeField] private Movement movementScript;
    [SerializeField] private MouseLook mouseLookScript;

    private bool orbMenuOpen = false;

    public void OpenOrbMenu()
    {
        if (orbMenuOpen) return;
        
        orbMenuOpen = true;
        
        if (gameplayUI != null) gameplayUI.SetActive(false);
        if (orbMenuUI != null) orbMenuUI.SetActive(true);
        
        if (movementScript != null) movementScript.enabled = false;
        if (mouseLookScript != null) mouseLookScript.enabled = false;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseOrbMenu()
    {
        if (!orbMenuOpen) return;
        
        orbMenuOpen = false;
        
        if (movementScript != null) movementScript.enabled = true;
        if (mouseLookScript != null) mouseLookScript.enabled = true;
        
        if (gameplayUI != null) gameplayUI.SetActive(true);
        if (orbMenuUI != null) orbMenuUI.SetActive(false);
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
