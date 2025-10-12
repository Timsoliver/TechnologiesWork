using UnityEngine;

[CreateAssetMenu(fileName = "New Button Data", menuName = "Interactables/ButtonData")]
public class ButtonData : ScriptableObject
{
    [Header("Button Color")]
    public Color chosenColor = Color.red;
    
    [Header("Interaction Settings")]
    public string targetObjectNameA;
    public string targetObjectNameB;

    [Header("Prompt Text")]
    public string description = "Interact";
    public string offDescription = "Activate portal";
    public string onDescription = "Deactivate portal";
}
