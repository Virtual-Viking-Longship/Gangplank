using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout : MonoBehaviour
{
    public float fadetime = 1f;
    public UnityEngine.UI.Image mask;


    public void Fade() => StartCoroutine(FadeIE());

    public IEnumerator FadeIE()
    {
        for(float i =fadetime; i>0; i-= Time.deltaTime)
        {
            mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, i / fadetime);
            yield return new WaitForEndOfFrame();
        }

        mask.gameObject.SetActive(false);
    }
}
