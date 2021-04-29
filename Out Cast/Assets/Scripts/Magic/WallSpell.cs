using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallSpell : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject wall;
    [SerializeField] float wallOffset = 0.5f;
    [SerializeField] GameObject wallSpawnMarker;
    float markerOffset = .1f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxRange;
    Vector3 spawnPos;
    bool spawnable = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        RaycastOnClick();

        if (Input.GetMouseButtonUp(0) && spawnable)
        {
            wallSpawnMarker.SetActive(false);
            wall.SetActive(true);
            wall.transform.position = spawnPos;
            spawnable = false;
        }
    }

    private void RaycastOnClick()
    {
        if (Input.GetMouseButton(0))
        {           
            //Ray ray = cam.ScreenPointToRay(new Vector3(cam.scaledPixelWidth / 2, cam.scaledPixelHeight / 2));
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxRange+10, layerMask))
            {
                if (hit.distance <= maxRange)
                {
                    spawnable = true;
                    wallSpawnMarker.SetActive(true);
                    wallSpawnMarker.transform.position = new Vector3(hit.point.x, hit.point.y + markerOffset, hit.point.z);
                    spawnPos = new Vector3(hit.point.x, hit.point.y + wallOffset, hit.point.z);
                }
                else if (hit.distance > maxRange)
                {
                    wallSpawnMarker.SetActive(false);
                    spawnable = false;
                }
            }
        }
    }


    void PlaceWall()
    {
        wall.transform.position = spawnPos;
    }
}
