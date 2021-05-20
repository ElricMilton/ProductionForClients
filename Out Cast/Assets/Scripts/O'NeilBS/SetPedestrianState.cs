using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class SetPedestrianState : MonoBehaviour
{
    public RangeSensor rangeSensor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPedestrianCower()
    {
        var allPedestrian = rangeSensor.GetDetected();
        foreach(GameObject ped in allPedestrian)
        {
            ped.GetComponent<PedestrianBehaviour>().CowerCheck();
        }

        var peddestrian = rangeSensor.GetNearest();
        peddestrian.GetComponent<PedestrianBehaviour>().GetCopCheck();
    }

}
