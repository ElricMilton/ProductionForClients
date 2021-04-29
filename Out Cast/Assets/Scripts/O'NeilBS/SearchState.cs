using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : MonoBehaviour
{

    public float speed;
    public float waitTime;
    public float startWaitTime;
    public float timerDecrease = 1f;
    public GameObject patrolPoint;

    public Vector3 moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        Instantiate(patrolPoint);
        waitTime = startWaitTime;
        //set inital position to move too 
        moveSpot = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
        //Looks at target
        //transform.LookAt(moveSpot);
        //TurnToLook();
    }


    void Update()
    {
        TurnToLook();
        Move();
    }

    bool _canWalk = false;
    void Move()
    {
        if (_canWalk)
        {
            //moves to new position 
            transform.position = Vector3.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
            _canWalk = false;
        }

        if (Vector3.Distance(transform.position, moveSpot) < 0.2f)
        {
            //when wait time is up create new postion to move too
            if (waitTime <= 0)
            {
                moveSpot = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
                waitTime = startWaitTime;
            }
            //decreses wait time in seconds
            else
            {
                waitTime -= (timerDecrease * Time.deltaTime);
            }
        }

        //if (transform.position == moveSpot)
        //{
        //    moveSpot = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
        //}
    }

    [SerializeField] float _lerpDuration = 3f;
    [SerializeField] float _angleLimit = 0f;
    void TurnToLook()
    {
        Vector3 direction = moveSpot - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _lerpDuration * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, toRotation) == _angleLimit)
        {
            _canWalk = true;
        }
    }
}
