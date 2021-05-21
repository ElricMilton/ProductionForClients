using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class ChangePedestrianStatus : MonoBehaviour
{
    public RangeSensor rangeSensor;
    private List<GameObject> _allPedestrians;
    bool _toCower = false;

    public BoolVariable _isPlayerChasable;

    private void Update()
    {
        if(!_toCower && _isPlayerChasable.Value)
        {
            _allPedestrians = rangeSensor.GetDetected();
            foreach (GameObject ped in _allPedestrians)
            {
                ped.GetComponent<PedestrianBehaviour>().CowerCheck();
            }

            var peddestrian = rangeSensor.GetNearest();
            peddestrian.GetComponent<PedestrianBehaviour>().GetCopCheck();

            _toCower = true;
        }
    }

    public void SetPedestrianCower()
    {
        _toCower = false;
    }

}
