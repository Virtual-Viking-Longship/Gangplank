using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandMenuChecker : MonoBehaviour
{
    public OVRHand Hand;
    // Update is called once per frame
    void Update()
    {
        if (Hand.IsSystemGestureInProgress)
        {
            GameObject Menu = GameObject.FindWithTag("Menu");
            Menu.SetActive(true);
        }
    }
}
