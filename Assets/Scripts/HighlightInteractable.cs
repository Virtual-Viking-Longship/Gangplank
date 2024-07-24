using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HighlightInteractable : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    public void OnSelectEnter()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = highlightColor;
        }
    }

    public void OnSelectExit()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}
