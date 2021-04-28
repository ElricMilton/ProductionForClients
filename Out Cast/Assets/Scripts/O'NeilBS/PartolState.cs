using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartolState : MonoBehaviour
{

    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;

        moveSpot.position = new Vector3(Random)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
