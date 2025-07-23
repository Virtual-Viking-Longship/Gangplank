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
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
