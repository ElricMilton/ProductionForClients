using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallSpell : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject wallSpawnMarker;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float heightOffset;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        RaycastOnClick();
    }

    private void RaycastOnClick()
    {
        if (Input.GetMouseButton(0))
        {           
            //Ray ray = cam.ScreenPointToRay(new Vector3(cam.scaledPixelWidth / 2, cam.scaledPixelHeight / 2));
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, layerMask) )
            {
                wallSpawnMarker.transform.position = new Vector3(hit.point.x, hit.point.y+heightOffset, hit.point.z);
            }
        }
    }
}
