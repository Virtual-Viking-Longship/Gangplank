using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainRenderer : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject[] targets;
 

    // Update is called once per frame
    void Update()
    {
        lr.positionCount = 1 + targets.Length;
        lr.SetPosition(0, this.transform.position);
        for(int i = 0; i < targets.Length; i++)
        {
            lr.SetPosition(i + 1, targets[i].transform.position);
        }
    }
}
