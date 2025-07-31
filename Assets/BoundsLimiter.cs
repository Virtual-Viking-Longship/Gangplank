using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsLimiter : MonoBehaviour
{
    public Transform Oar;
    private Vector3 startposition;
    // Start is called before the first frame update
    void Start()
    {
        startposition = Oar.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = Oar.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, startposition.x - 0.1f, startposition.x + 0.1f);
        currentPosition.y = Mathf.Clamp(currentPosition.y, startposition.y - 0.4f, startposition.y + 0.4f);
        currentPosition.z = Mathf.Clamp(currentPosition.z, startposition.z - 0.6f, startposition.z + 0.4f);

        Oar.position = currentPosition;
    }
}
