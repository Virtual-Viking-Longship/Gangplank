using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;

/*
This class is added to an object as a component.
*/
public class ObjectInspector : MonoBehaviour
{
    [SerializeField] public GameObject infoPanel;
    public Color highlightColor = Color.yellow;
    public Outline outline;

    private void Start()
    {
        // Transform goal = gameObject.transform;
        // TextAsset document = Resources.Load<TextAsset>("Ship-Annotations/annotations/" + goal.name);
        // infoPanel.GetComponentInChildren<FormattedDocumentDisplay>().DisplayDocument(document);
    }

    public void OnSelectEnter()
    {
        outline.enabled = true;
    }

    public void OnSelectExit()
    {
        outline.enabled = false;
    }

    public void SendInfoPanel()
    {

        Transform goal = gameObject.transform;
        TextAsset document = Resources.Load<TextAsset>("Ship-Annotations/annotations/" + goal.name);
        if (document == null) return;

        infoPanel.GetComponent<CanvasGroup>().alpha = 1;
        infoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        infoPanel.GetComponent<CanvasGroup>().interactable = true;
        infoPanel.SetActive(true);
        //is there a better way to do this?
        // infoPanel.GetChild(0).GetChild(0).GetComponent<BoxCollider>().enabled = true;        //for old info panel structure
        //infoPanel.GetChild(1).GetComponent<BoxCollider>().enabled = true;                       //for new info panel structure

        infoPanel.GetComponentInChildren<FormattedDocumentDisplay>().DisplayDocument(document);

        Transform follow = GameObject.FindWithTag("MainCamera").transform;
        infoPanel.transform.position = follow.position + new Vector3(follow.forward.x, 0, follow.forward.z).normalized * 0.5f;
        infoPanel.transform.LookAt(new Vector3(follow.position.x, infoPanel.transform.position.y, follow.position.z));
        infoPanel.transform.forward *= -1;
    }
}
