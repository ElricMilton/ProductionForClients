using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class GuardBehaviourV2 : MonoBehaviour
{
    //Guard States
    public bool isPatroling;
    public bool isReturningToPost;
    public bool isSearching;
    public bool isChasing;
    public PlayerStatus ps;

    // public RangeSensor sensor;
    public TriggerSensor fov;
    public GameObject post;
    public float chaseSpeed = 4f;

    //for searching state
    public float speed;
    public float waitTime;
    public float startWaitTime;
    public float timerDecrease = 1f;
    public GameObject patrolPoint;

    //search timer vars
    public float searchTime;
    public float startSearchTime;

    public Vector3 moveSpot;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    public enum GameStates
    {
        t1,
        t2

    }
    GameStates gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameStates.t1;
        waitTime = startWaitTime;
        searchTime = startSearchTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameStates.t1:
                Debug.Log("We are in state 1!");
                break;
            case GameStates.t2:
                Debug.Log("We are in state 2!");
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            gameState = GameStates.t2;
        }



        var player = fov.GetNearest();
        if (player != null & ps.isChaseable == true)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            var deteced = fov.GetNearest();
            if (deteced != null)
            {
                chase(deteced);
            }
            if(isChasing == true & deteced == null & isSearching == false)
            {

                Instantiate(patrolPoint);
                moveSpot = new Vector3((Random.Range(minX, maxX) + patrolPoint.transform.position.x), patrolPoint.transform.position.y, (Random.Range(minY, maxY) + patrolPoint.transform.position.z));
                isSearching = true;
            }
        }

        if (isSearching)
        {
            isChasing = false;
            if (searchTime >0)
            {
                Move();
                TurnToLook();
                searchTime -= (timerDecrease * Time.deltaTime);
            }
            else
            {
                searchTime = startSearchTime;
                isSearching = false;
                isReturningToPost = true;
            }

        }



        if (isReturningToPost == true)
        {
            ReturnToPost();

        }

    }


    void chase(GameObject target)
    {
        var deteced = fov.GetNearest();
        if (deteced != null)
        {
            transform.LookAt(target.transform);
            if ((transform.position - target.transform.position).magnitude > 2f)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                return;
            }

        }
        //var speed = 4f;

        //transform.LookAt(target.transform);
        //if ((transform.position - target.transform.position).magnitude > 2f)
        //{
        //    transform.position += transform.forward * speed * Time.deltaTime;
        //}
        //else
        //{
        //    return;
        //}


    }

    void ReturnToPost()
    {

        if ((transform.position - post.transform.position).magnitude > 2f)
        {
            var speed = 4f;

            transform.LookAt(post.transform, Vector3.up);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            isChasing = false;
            isSearching = false;
            isReturningToPost = false;
            return;
        }

    }


    public bool _canWalk = false;
    void Move()
    {
        if (_canWalk == true)
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
        //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, toRotation.y, 0, 0), _lerpDuration * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _lerpDuration * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, toRotation) == _angleLimit)
        {
            _canWalk = true;
        }
    }


}
