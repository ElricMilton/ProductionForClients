using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpell : MonoBehaviour
{
    [SerializeField] GameObject wallSpawnMarker;
    [SerializeField] float wallSpawnRange;
    Camera cam;
    [SerializeField] LayerMask groundLayer;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DoRaycast();
        }
    }
    private void OnMouseDown()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.scaledPixelWidth / 2, cam.scaledPixelHeight / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hitInfo, groundLayer))
        {
            if (hitInfo.distance <= wallSpawnRange)
            {
                wallSpawnMarker.SetActive(true);
                wallSpawnMarker.transform.position = hitInfo.point;
            }
            else if (hitInfo.distance > wallSpawnRange)
            {
                wallSpawnMarker.SetActive(false);
            }
        }
    }
    void DoRaycast()
    {

    }
}
