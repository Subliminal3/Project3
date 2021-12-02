using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRTest : MonoBehaviour
{
    private LineRenderer laser;

    public GameObject start, end;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();

        laser.SetPosition(0, start.transform.position);
        laser.SetPosition(1, end.transform.position);
    }
}
