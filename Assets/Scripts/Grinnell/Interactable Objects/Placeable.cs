using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;


/*
This is a class for a placeable object, which can be told to translate to a certain position and rotation
The ship pieces that spawn in the ship building scene have this class
You should set a onPlace event that is called when the piece finishes translating to its target
In the ship building scene, this is done in code after spawning another piece
*/
public class Placeable : MonoBehaviour
{
    public bool onPlayerHand = false;
    public UnityEvent onPlace;
    public Coroutine translateCoroutine = null;
    public Vector3 originalPos;
    public Quaternion originalRot;
    public DistanceHandGrabInteractable xrInteractable;


    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;

        xrInteractable = GetComponent<DistanceHandGrabInteractable>();
    }

    // This is meant to reset the position when the piece falls out of some bounds
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") TranslateToStart(2f);
    }

    public void OnPlayerPickUp() { onPlayerHand = true; }
    public void OnPlayerRelease() { onPlayerHand = false; }

    public void TranslateToFinalTarget(Transform target, float translateTime)
    {
        if(translateCoroutine == null && !onPlayerHand) translateCoroutine = StartCoroutine(TranslateToTargetCoroutine(target.position, target.rotation, translateTime, true));
    }

    public void TranslateToStart(float translateTime)
    {
        if(translateCoroutine == null && !onPlayerHand) translateCoroutine = StartCoroutine(TranslateToTargetCoroutine(originalPos, originalRot, translateTime, false));
    }

    private IEnumerator TranslateToTargetCoroutine(Vector3 targetPos, Quaternion targetRot, float translateTime, bool targetIsFinal)
    {
        xrInteractable.enabled = false;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        GetComponentInParent<Rigidbody>().isKinematic = true;
        float time = 0;
        while(time < translateTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / translateTime);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, time / translateTime);
            yield return null;
            time += Time.deltaTime;
        }
        
        if (targetIsFinal)
        {
            onPlace?.Invoke();
            Destroy(gameObject);
        }

        GetComponentInParent<Rigidbody>().isKinematic = false;
        xrInteractable.enabled = true;
        translateCoroutine = null;
    }
}
