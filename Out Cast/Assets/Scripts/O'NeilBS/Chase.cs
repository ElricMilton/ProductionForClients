using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class Chase : MonoBehaviour
{
   // public RangeSensor sensor;
    public TriggerSensor fov;
    //public Animator animator;
    public GameObject post;

    public float chaseSpeed = 4f;



    // Update is called once per frame
    void Update()
    {
        var deteced = fov.GetNearest();
        if (deteced != null)
        {
            chase(deteced);
        }
        else
        {
            idle();
        }

    }

    void chase(GameObject target)
    {
        var speed = 4f;

        transform.LookAt(target.transform);
        if((transform.position - target.transform.position).magnitude > 2f)
        {
        transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            return;
        }


    }

    void idle()
    {
     if ((transform.position - post.transform.position).magnitude > 2f)
         {
         var speed = 4f;

         transform.LookAt(post.transform, Vector3.up);
         transform.position += transform.forward * speed * Time.deltaTime;
     }
     else
     {
         return;
     }
    }
}
