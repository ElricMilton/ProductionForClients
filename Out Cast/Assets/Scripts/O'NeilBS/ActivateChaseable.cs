using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChaseable : MonoBehaviour
{
    public GameObject detector;
    public bool canDetect = true;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            changeDetection();
        }
    }


    void changeDetection()
    {
        if(canDetect == true)
        {
            detector.SetActive(false);
            canDetect = false;
        }
        else
        {
            detector.SetActive(true);
            canDetect = true;
        }
    }
}
