using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.XR.Interaction.Toolkit.UI;
 
// using OculusSampleFramework;
// using Oculus.Interaction;


/*
This class makes the player able to inspect objects
Inspection happens when the player is hovering an object with either right or left ray interactors and pressing the trigger
This class is meant to be attacthed to the player
*/
public class ObjectInspector : MonoBehaviour
{
//     [SerializeField] private InputActionReference leftInput = null, rightInput = null;
    [SerializeField] private Transform infoPanel;

    public void SendInfoPanel()
    {
        Debug.Log("plank inspector called");
        Transform goal = gameObject.transform;
        TextAsset document = Resources.Load<TextAsset>(goal.name);
        if (document == null) return;

        // infoPanel.GetComponent<LazyFollow>().target = goal;
        infoPanel.transform.position = goal.position + Vector3.up * 0.1f;
        infoPanel.GetComponent<CanvasGroup>().alpha = 1;
        infoPanel.GetComponent<CanvasGroup>().interactable = true;
        infoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        infoPanel.GetChild(0).GetChild(0).GetComponent<BoxCollider>().enabled = true;

        // infoPanel.GetComponentInChildren<FormattedDocumentDisplay>().DisplayDocument(document);
    }
    // [SerializeField] private RayInteractor left, right;

    // // void Start()
    // // {
    // //     leftInput.action.started += Inspect;
    // //     rightInput.action.started += Inspect;
    // // }
    // // void Start()
    // // {
    // //     // Register input actions for Oculus
    // //     OVRInput.Update += Inspect;
    // // }

    // // void OnDestroy()
    // // {
    // //     // Unregister input actions
    // //     OVRInput.Update -= Inspect;
    // // }

    // void Update()
    // {
    //     Inspect();
    // }

    // // void Inspect()
    // // {
    // //     if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
    // //     {
    // //         if (left.interactablesHovered.Count > 0) SendInfoPanel(left.interactablesHovered[0].transform);
    // //     }

    // //     if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
    // //     {
    // //         if (right.interactablesHovered.Count > 0) SendInfoPanel(right.interactablesHovered[0].transform);
    // //     }
    // // }

    // // void Inspect(InputAction.CallbackContext ctx)
    // // {
    // //     if (left.interactablesHovered.Count > 0) SendInfoPanel(left.interactablesHovered[0].transform);
    // //     if (right.interactablesHovered.Count > 0) SendInfoPanel(right.interactablesHovered[0].transform);
    // // }

    // void Inspect()
    // {
    //     if (left.InteractableSelected) SendInfoPanel(left.interactablesHovered[0].transform);
    //     if (right.InteractableSelected) SendInfoPanel(right.interactablesHovered[0].transform);
    // }

    // private void SendInfoPanel(Transform goal)
    // {
    //     TextAsset document = Resources.Load<TextAsset>(goal.name);
    //     // Debug.Log("goal.name = " + goal.name);
    //     if (document == null) return;

    //     infoPanel.GetComponent<LazyFollow>().target = goal;
    //     //infoPanel.transform.parent = goal;
    //     infoPanel.transform.position = goal.position + Vector3.up * 0.5f;
    //     infoPanel.GetComponent<CanvasGroup>().alpha = 1;
    //     infoPanel.GetComponent<CanvasGroup>().interactable = true;
    //     infoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    //     infoPanel.GetChild(0).GetChild(0).GetComponent<BoxCollider>().enabled = true;
    //     Debug.Log("hello");
    //     infoPanel.GetComponentInChildren<FormattedDocumentDisplay>().DisplayDocument(document, goal.name);
    // }
}