using System;
using UnityEngine;

public class TerrainManipulation : MonoBehaviour
{
    private Renderer materialRenderer;

    [SerializeField] private float speed;
    private void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        
        // materialRenderer.material.color = Color.red;      <----- Changing Colour of Materials
    }

    private void FixedUpdate()
    {
        if (materialRenderer != null)
        {
            Vector2 _textureOffsetVector2 = new Vector2(materialRenderer.material.GetTextureOffset("_BaseColorMap").x + speed, materialRenderer.material.GetTextureOffset("_BaseColorMap").y);
            materialRenderer.material.SetTextureOffset("_BaseColorMap", _textureOffsetVector2);
        }
    }
}
