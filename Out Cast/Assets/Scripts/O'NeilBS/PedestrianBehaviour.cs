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
    public GameObject player;

    public GameObject pedestrian;

    // public RangeSensor sensor;
    public TriggerSensor fov;
    public RangeSensor rangeSensor;

    //for searching state
    public Waypoint post;
    public float runSpeed = 4f;
    public float EnemyDistanceRun = 4f;

    //timer vars
    public float runTime;
    public float startRunTime;
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
        runTime = startRunTime;
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
                Debug.Log("We are in state running!");
                StopPatrol();
                Running();

                break;
            case GameStates.movingToCop:
                Debug.Log("We are in state movingToCop!");
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
            playerPos.playerLastPos = player.transform.position;
            runTime = startRunTime;
            gameState = GameStates.running;
            Debug.Log("found you");
        }

        //if (gameState == GameStates.running & player == null)
        //{
        //    gameState = GameStates.movingToCop;
        //}


    }

    void Running()
    {
        //var deteced = rangeSensor.GetNearest();
        //if (deteced.tag == "Player")
        //{
        //    RunFrom(deteced);
        //}
        //transform.LookAt(-player.transform.position);
        //if ((transform.position - player.transform.position).magnitude < 15f)
        //{
        //    //transform.position += transform.forward * speed * Time.deltaTime;
        //    var FromPlayer =  transform.position - player.transform.position;
        //    agent.SetDestination(FromPlayer);
        //    movementAnimator.SetFloat("Move", 1f);
        //}
        //else
        //{
        //    gameState = GameStates.movingToCop;
        //}


        float distance = Vector3.Distance(transform.position, player.transform.position);

        Debug.Log("distance" + distance);

        if(distance > EnemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }
    }

    //void RunFrom(GameObject target)
    //{
    //    playerPos.playerLastPos = target.transform.position;
    //    transform.LookAt(-target.transform.position);
    //    if ((transform.position + target.transform.position).magnitude > 15f)
    //    {
    //        //transform.position += transform.forward * speed * Time.deltaTime;
    //        agent.SetDestination(-target.transform.position);
    //        movementAnimator.SetFloat("Move", 1f);
    //    }
    //    else
    //    {
    //        return;
    //    }
    //}

    void MoveToCop()
    {

        var deteced = rangeSensor.GetNearest();
        if (deteced != null)
        {
            if ((transform.position - deteced.transform.position).magnitude > 5f)
            {

                //transform.LookAt(post.transform, Vector3.up);
                agent.SetDestination(deteced.transform.position);
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
