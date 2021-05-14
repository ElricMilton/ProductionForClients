﻿using System.Collections;
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
                MoveToCop();
                break;
            case GameStates.cowering:
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
        gameState = GameStates.movingToCop;
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
            if ((transform.position - detected.transform.position).magnitude > 3f)
            {
                agent.SetDestination(detected.transform.position);
                movementAnimator.SetFloat("Move", 2f);
            }
            else
            {
                deteced.GetComponent<GuardBehaviourV2>().SearchStateTransition();
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
        //play the cower animation here
        }
        else
        {
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
