using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInvisibler : MonoBehaviour
{
    public Material HandMat;
    public float ogopacity;
    public float ogopacityf;
    public void HideHands(float delay) => StartCoroutine(HideHandsIE(delay));

    public void ShowHands(float delay) => StartCoroutine(ShowHandsIE(delay));

   
    public IEnumerator HideHandsIE(float delay = .5f)
    {
        ogopacity = HandMat.GetFloat("_Opacity");
        ogopacityf = HandMat.GetFloat("_OutlineOpacity");
        for (float f = 0; f < delay; f += Time.deltaTime)
        {
            HandMat.SetFloat("_Opacity", ogopacity * ((delay - f)/delay));
            HandMat.SetFloat("_OutlineOpacity", ogopacity * ((delay - f) / delay));
            yield return new WaitForFixedUpdate();
        }
        HandMat.SetFloat("_Opacity", 0);
        HandMat.SetFloat("_OutlineOpacity", 0);
    }
    public IEnumerator ShowHandsIE(float delay = .5f)
    {
        for (float f = 0; f < delay; f += Time.deltaTime)
        {
            HandMat.SetFloat("_Opacity", ogopacity * (f/delay));
            HandMat.SetFloat("_OutlineOpacity", ogopacity * (f / delay));
            yield return new WaitForFixedUpdate();
        }
        HandMat.SetFloat("_Opacity", ogopacity);
        HandMat.SetFloat("_OutlineOpacity", ogopacityf);
    }
}
