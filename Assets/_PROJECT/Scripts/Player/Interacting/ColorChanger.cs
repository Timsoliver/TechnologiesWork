using UnityEngine;
using UnityEngine.EventSystems;

public class ColorChanger : MonoBehaviour, IInteractable
{
    Material mat;
    
    public bool useRandomColor = false;
    public Color chosenColor = Color.red;

    public void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    public string GetDescription()
    {
        return useRandomColor ? "Change to random color" : "Change Color";
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name} was interacted with!");
        
        if (useRandomColor)
        {
            mat.color = new Color(Random.value, Random.value, Random.value);
        }
        else
        {
            mat.color = chosenColor;
        }
    }
}

