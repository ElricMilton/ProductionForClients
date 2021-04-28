using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartolState : MonoBehaviour
{

    public float speed;
    public float waitTime;
    public float startWaitTime;
    public float timerDecrease = 1f;
    public GameObject patrolPoint;

    public Transform moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        Instantiate(patrolPoint);

        waitTime = startWaitTime;
        //set inital position to move too 
        //moveSpot.position = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
        moveSpot.position = new Vector3(Random.Range(minX, maxX), patrolPoint.transform.position.y, Random.Range(minY, maxY));
    }


    void Update()
    {
        //moves to new position 
        transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, moveSpot.position) < 0.2f)
        {
            //when wait time is up create new postion to move too
            if(waitTime <= 0)
            {
                //moveSpot.position = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
                moveSpot.position = new Vector3(Random.Range(minX, maxX), patrolPoint.transform.position.y, Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            //decreses wait time in seconds
            else
            {
                waitTime -= (timerDecrease * Time.deltaTime);
            }
        }
    }
}
