using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class MenuManager : MonoBehaviour
{
    public Transform head;
    public Transform hand;
    public GameObject menu;
    public InputActionProperty showButton;
    public float spawnDistance = 1;
    // Start is called before the first frame update
    public void Show()
    {
        menu.SetActive(!menu.activeSelf);
        menu.GetComponent<LazyFollow>().enabled = (!menu.GetComponent<LazyFollow>().enabled);
        menu.transform.position = hand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = hand.rotation * new Vector3(0.055f, 0, 0);
        Vector3 palm = new Vector3((hand.position.x), hand.position.y, hand.position.z) + offset;
        float dist = Vector3.Distance(palm, head.position)/5;
        Vector3 vmenu = Vector3.MoveTowards(palm, head.position, dist);
        menu.transform.position = vmenu;
        menu.transform.LookAt(new Vector3(head.position.x, head.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
