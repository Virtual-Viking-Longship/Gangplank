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
    public Transform target = null;

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

    /* 
    This function is called when the player inspects the object
    It will display the info panel with the document related to this object
    The document is expected to be in the Resources/Ship-Annotations/annotations folder with
    the same name as the object this script is attached to, e.g. "mast.md" for an object named "mast"  
    */
    public void SendInfoPanel()
    {
        // First get the document related to this object
        // If the document is not found, it will return and not display the info panel
        Transform goal = gameObject.transform;
        TextAsset document = Resources.Load<TextAsset>("Ship-Annotations/annotations/" + goal.name);
        if (document == null) return;

        // If present, load the audio clip related to this object and play immediately
        AudioClip audioClip = Resources.Load<AudioClip>("Ship-Annotations/audio/" + goal.name);
        if (audioClip != null)
        {
            infoPanel.GetComponentInChildren<AudioSource>().clip = audioClip;
            PlayAudio();
        }

        // Position the info panel relative to target, if set, else in front of the camera
        if (target != null)
        {
            infoPanel.transform.position = target.position + new Vector3(0.5f, 1.1f, -1.0f) * 0.5f;
        }
        else
        {
            Transform follow = GameObject.FindWithTag("MainCamera").transform;
            infoPanel.transform.position = follow.position + new Vector3(follow.forward.x, 0, follow.forward.z).normalized * 0.5f;
            infoPanel.transform.LookAt(new Vector3(follow.position.x, infoPanel.transform.position.y, follow.position.z));
            infoPanel.transform.forward *= -1;
        }

        // Show the info panel
        infoPanel.GetComponent<CanvasGroup>().alpha = 1;
        infoPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        infoPanel.GetComponent<CanvasGroup>().interactable = true;
        infoPanel.SetActive(true);

        // Finally, display the document in the info panel
        infoPanel.GetComponentInChildren<FormattedDocumentDisplay>().DisplayDocument(document);

    }

    // Plays sound, if present
    public void PlayAudio()
    {
        infoPanel.GetComponentInChildren<AudioSource>().Play();
    }
}
