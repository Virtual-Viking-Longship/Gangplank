using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowItem : MonoBehaviour
{
    public MeshRenderer targetObject;
    private GameObject headset;
    public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        headset = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 offset = (this.transform.position - headset.transform.position).normalized;
        Vector3 itemPos = targetObject.bounds.center + new Vector3(0, targetObject.bounds.size.y/2 + .15f, 0f) + offset * .25f;
        Vector3 smoothPos = Vector3.Lerp(transform.position, itemPos, smoothSpeed);
        transform.position = smoothPos;
        transform.rotation = Quaternion.LookRotation(offset);
    }
}
