using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class PedestrianBehaviour : MonoBehaviour
{

    public BoolVariable chaseSatus;

    public Animator movementAnimator;
    public PlayerStatus playerPos;
    public NavMeshAgent agent;

    public GameObject pedestrian;

    // public RangeSensor sensor;
    public TriggerSensor fov;
    public RangeSensor rangeSensor;

    //for searching state
    public Waypoint post;
    public float runSpeed = 4f;

    //timer vars
    public float searchTime;
    public float startSearchTime;
    public float timerDecrease = 1f;

    public enum GameStates
    {
        patroling,
        running,
        movingToCop,
    }
    GameStates gameState;
    void Start()
    {
        StartPatrol();
        gameState = GameStates.patroling;
        searchTime = startSearchTime;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameStates.patroling:
                Debug.Log("We are in state patroling!");
                StartPatrol();
                break;
            case GameStates.running:
                Debug.Log("We are in state chasing!");
                Running();
                StopPatrol();
                break;
            case GameStates.movingToCop:
                Debug.Log("We are in state searching!");
                MoveToCop();
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameState = GameStates.patroling;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameState = GameStates.running;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameState = GameStates.movingToCop;
        }



        var player = fov.GetNearest();
        if (player != null & chaseSatus.Value == true)
        {
            searchTime = startSearchTime;
            gameState = GameStates.running;
        }

        if (gameState == GameStates.running & player == null)
        {
            gameState = GameStates.movingToCop;
        }


    }

    void Running()
    {
        var deteced = rangeSensor.GetNearest();
        if (deteced != null)
        {
            RunFrom(deteced);
        }
    }

    void RunFrom(GameObject target)
    {
        playerPos.playerLastPos = target.transform.position;
        transform.LookAt(target.transform.position);
        if ((transform.position + target.transform.position).magnitude > 15f)
        {
            //transform.position += transform.forward * speed * Time.deltaTime;
            agent.SetDestination(-target.transform.position);
            movementAnimator.SetFloat("Move", 1f);
        }
        else
        {
            return;
        }
    }

    void MoveToCop()
    {

        var deteced = rangeSensor.GetNearest();
        if (deteced.tag == "Cop")
        {
            if ((transform.position - post.transform.position).magnitude > 5f)
            {

                //transform.LookAt(post.transform, Vector3.up);
                agent.SetDestination(post.transform.position);
                //transform.position += transform.forward * speed * Time.deltaTime;
                movementAnimator.SetFloat("Move", 0.5f);
            }
        }


        else
        {
            gameState = GameStates.patroling;
        }
    }


    void StartPatrol()
    {
        movementAnimator.SetFloat("Move", 0.5f);
        pedestrian.GetComponent<PedestrianNavigationController>().enabled = true;
        pedestrian.GetComponent<WaypointNavigator>().enabled = true;
        pedestrian.GetComponent<NavMeshAgent>().enabled = false;
    }
    void StopPatrol()
    {
        pedestrian.GetComponent<PedestrianNavigationController>().enabled = false;
        pedestrian.GetComponent<WaypointNavigator>().enabled = false;
        pedestrian.GetComponent<NavMeshAgent>().enabled = true;
    }


}
