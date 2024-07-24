using UnityEngine;
using TMPro;

public class ChangeTextColor : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public void ChangeColor()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            textMeshPro.color = new Color(1.0f, 0f, 0f);
        }
    }
}
