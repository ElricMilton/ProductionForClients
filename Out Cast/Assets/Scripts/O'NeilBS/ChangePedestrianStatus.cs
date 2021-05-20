
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class SetPedestrianState : MonoBehaviour
{
    public RangeSensor rangeSensor;

    public void SetPedestrianCower()
    {
        var allPedestrian = rangeSensor.GetDetected();
        foreach (GameObject ped in allPedestrian)
        {
            ped.GetComponent<PedestrianBehaviour>().CowerCheck();
        }

        var peddestrian = rangeSensor.GetNearest();
        peddestrian.GetComponent<PedestrianBehaviour>().GetCopCheck();
    }

}
