﻿using UnityEngine;
using System.Collections;

public class BillboardMoon : MonoBehaviour
{
    public Camera cam;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform);
    }
}
