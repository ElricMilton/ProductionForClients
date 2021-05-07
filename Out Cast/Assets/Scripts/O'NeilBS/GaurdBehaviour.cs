using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class GaurdBehaviour : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public TriggerSensor fov;

    public float searchTime;
    public float startSearchTime;


    public bool isPatroling;
    public bool isSearching;
    public bool isChasing;


    // Start is called before the first frame update
    void Start()
    {
        isPatroling = true;
        isSearching = false;
        isChasing = false;
        searchTime = startSearchTime;
    }

    // Update is called once per frame
    void Update()
    {
        var deteced = fov.GetNearest();
        //if (deteced != null & ps.isChaseable == true)
        {
            enemy.GetComponent<ChaseState>().enabled = true;
            //enemy.GetComponent<SearchState>().enabled = false;
            enemy.GetComponent<PatrolState>().enabled = false;
            isChasing = true;
            isSearching = false;
            isPatroling = false;
        }
        if(isChasing == true & deteced == null & isSearching == false)
        {
            enemy.GetComponent<ChaseState>().enabled = false;
            //enemy.GetComponent<SearchState>().enabled = true;
            enemy.GetComponent<PatrolState>().enabled = false;
            isChasing = false;
            isSearching = true;
            isPatroling = false;

        }
        if (isSearching == true & deteced == null )
        {

            if (searchTime <= 0f)
            {
                searchTime = startSearchTime;
                enemy.GetComponent<ChaseState>().enabled = false;
                //enemy.GetComponent<SearchState>().enabled = false;
                enemy.GetComponent<PatrolState>().enabled = true;
                isChasing = false;
                isSearching = false;
                isPatroling = true;
            }
            else
            {
                searchTime -= (1f * Time.deltaTime);
            }
        }

    }


    /* void searching()
     {

         if (searchTime <= 0f)
         {
             searchTime = startSearchTime;
             enemy.GetComponent<ChaseState>().enabled = false;
             enemy.GetComponent<SearchState>().enabled = false;
             enemy.GetComponent<PatrolState>().enabled = true;
             isChasing = false;
             isSearching = false;
             isPatroling = true;
         }
         else
         {
             searchTime -= (1f * Time.deltaTime);
         }
     }*/

    //from guard behaviour v1


    public Vector3 moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;



    //public bool _canWalk = false;
    //void Move()
    //{
    //    if (_canWalk == true)
    //    {
    //        //moves to new position 
    //        transform.position = Vector3.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
    //        _canWalk = false;
    //    }

    //    if (Vector3.Distance(transform.position, moveSpot) < 0.2f)
    //    {
    //        //when wait time is up create new postion to move too
    //        if (waitTime <= 0)
    //        {
    //            moveSpot = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
    //            waitTime = startWaitTime;
    //        }
    //        //decreses wait time in seconds
    //        else
    //        {
    //            waitTime -= (timerDecrease * Time.deltaTime);
    //        }
    //    }

    //    //if (transform.position == moveSpot)
    //    //{
    //    //    moveSpot = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
    //    //}
    //}

    //[SerializeField] float _lerpDuration = 3f;
    //[SerializeField] float _angleLimit = 0f;
    //void TurnToLook()
    //{
    //    Vector3 direction = moveSpot - transform.position;
    //    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, toRotation.y, 0, 0), _lerpDuration * Time.deltaTime);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _lerpDuration * Time.deltaTime);

    //    if (Quaternion.Angle(transform.rotation, toRotation) == _angleLimit)
    //    {
    //        _canWalk = true;
    //    }
    //}
}
