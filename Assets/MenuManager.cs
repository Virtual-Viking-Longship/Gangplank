using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public Transform head;
    public GameObject menu;
    public InputActionProperty showButton;
    public float spawnDistance = 1;
    // Start is called before the first frame update
    public void Show()
    {
        menu.SetActive(!menu.activeSelf);

        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
    }

    // Update is called once per frame
    void Update()
    {
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
