using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableArea : MonoBehaviour
{
    [SerializeField] BoolVariable isChasable;
    [SerializeField] BoolVariable isDischarging;
    GameObject player;

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isChasable.Value = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isChasable.Value = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            isChasable.Value = false;
            isDischarging.Value = false;
        }
    }
}
