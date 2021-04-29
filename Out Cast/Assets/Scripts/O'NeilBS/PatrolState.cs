using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    public GameObject post;

    void Update()
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
