using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLookAtCam : MonoBehaviour
{
    Transform cam;
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
