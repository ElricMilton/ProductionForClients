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
    public BoolVariable isPlayerChasable;

    public GameObject pedestrian;

    // public RangeSensor sensor;
    public TriggerSensor fov;
    public RangeSensor rangeSensor;

    //for searching state
    public float runSpeed = 4f;
    public float EnemyDistanceRun = 4f;

    //timer vars
    public float runTime;
    public float startRunTime;
    public float timerDecrease = 1f;

    public BoolVariable areCopsAlerted;

    public enum GameStates
    {
        patroling,
        running,
        movingToCop,
        cowering,
    }
    GameStates gameState;

    void Start()
    {
        StartWander();
        gameState = GameStates.patroling;
        runTime = startRunTime;
        cowerTime = startCowerTime;
    }

    void Update()
    {
        switch (gameState)
        {
            case GameStates.patroling:
                Debug.Log("We are in state patroling!");
                StartWander();
                break;
            case GameStates.running:
                Debug.Log("We are in state running!");
                StopWander();
                Running();
                break;
            case GameStates.movingToCop:
                Debug.Log("We are in state movingToCop!");
                StopWander();
                MoveToCop();
                break;
            case GameStates.cowering:
                RunFrom();
                Cower();
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
            Debug.Log("found you");
        }
    }

    public void GetCopTransition()
    {
        if (isPlayerChasable.Value == true && chaseSatus.Value == true)
        {
            gameState = GameStates.movingToCop;
        }
        else if (isPlayerChasable.Value == true)
        {
            gameState = GameStates.cowering;
        }
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

        //Debug.Log("distance" + distance);

        if(distance > EnemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }
    }

    void RunFrom()
    {
        var detected = rangeSensor.GetNearest();
        transform.LookAt(-detected.transform.position);
        if ((transform.position + detected.transform.position).magnitude > 15f)
        {
            agent.SetDestination(-detected.transform.position);
            movementAnimator.SetFloat("Move", 1f);
        }
    }

    void MoveToCop()
    {
        var detected = rangeSensor.GetNearest();
        if (detected != null && areCopsAlerted.Value == false)
        {
            if ((transform.position - detected.transform.position).magnitude > 1.5f)
            {
                agent.SetDestination(detected.transform.position);
                agent.speed = runSpeed;
                movementAnimator.SetFloat("Move", 2f);
            }
            else if ((transform.position - detected.transform.position).magnitude < 1.5f)
            {
                agent.SetDestination(transform.position);
                detected.GetComponent<GuardBehaviourV2>().SearchStateTransition();
                gameState = GameStates.cowering;
            }
        }

    }

    public float cowerTime;
    public float startCowerTime = 5f;
    void Cower()
    {
        if(cowerTime > 0)
        {
            agent.speed = 0;
            cowerTime -= 1 * Time.deltaTime;
            //play the cower animation here
        }
        else
        {
            agent.speed = 1;
            cowerTime = startCowerTime;
            gameState = GameStates.patroling;
        }
    }

    void StartWander()
    {
        movementAnimator.SetFloat("Move", 1f);
        pedestrian.GetComponent<PedestrianWander>().enabled = true;
    }
    void StopWander()
    {
        pedestrian.GetComponent<PedestrianWander>().enabled = false;
    }

}
