﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class PedestrianBehaviour : MonoBehaviour
{
    public Animator movementAnimator;
    public PlayerStatus playerPos;
    public NavMeshAgent agent;
    public GameObject player;
    public BoolVariable isPlayerChasable;
    public BoolVariable isAlerted;

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
    public BoolVariable onAlert;

    public OverheadStateSwitcher overheadStates;
    AudioSource alertSound;

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
        alertSound = GetComponent<AudioSource>();
        if(onAlert.Value == true)
        {
        onAlert.Value = false;
        }
        else
        {
            return;
        }

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
                RunFrom();
                break;
            case GameStates.movingToCop:
                Debug.Log("We are in state movingToCop!");
                StopWander();
                MoveToCop();
                break;
            case GameStates.cowering:
                Debug.Log("Cowering");
                Cower();
                break;
            default:
                break;
        }
    }
    public bool hasSeenPlayer = false;
    public void SeePlayer()
    {
        if(gameState == GameStates.cowering)
        {
        var player = fov.GetNearest();
        playerPos.playerLastPos = player.transform.position;
        }
    }
    //public void DontSeePlayer()
    //{
    //    hasSeenPlayer = false;
    //}

    public void GetCopTransition()
    {
        if (isPlayerChasable.Value == true)
        {
            if (onAlert.Value == false)
            {
                GetCopCheck();
            }
        }
    }

    //void GetCopCheck()
    //{
    //    if (isPlayerChasable.Value == true && onAlert.Value == false)
    //    {
    //        var player = fov.GetNearest();
    //        playerPos.playerLastPos = player.transform.position;
    //        onAlert.Value = true;
    //        alertSound.Play();
    //        gameState = GameStates.movingToCop;
    //    }
    //}

    //public void CowerCheck()
    //{
    //    if (isPlayerChasable.Value == true && onAlert.Value == true)
    //    {
    //        gameState = GameStates.cowering;
    //    }
    //}

    public void GetCopCheck()
    {
        //var player = fov.GetNearest();
        //playerPos.playerLastPos = player.transform.position;
        //onAlert.Value = true;
        alertSound.Play();
        gameState = GameStates.movingToCop;
    }

    public void CowerCheck()
    {
        gameState = GameStates.cowering;
    }

    void Running()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

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
        if ((transform.position + detected.transform.position).magnitude > 5f)
        {
            agent.SetDestination(-detected.transform.position);
            movementAnimator.SetFloat("Move", 1f);
            overheadStates.OverheadCowerState();
            onAlert.Value = false;
        }
        else
        {
            gameState = GameStates.cowering;
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
                overheadStates.OverheadAlertState();
            }
            else if ((transform.position - detected.transform.position).magnitude < 1.5f)
            {
                agent.SetDestination(transform.position);
                detected.GetComponent<GuardBehaviourV2>().SearchStateTransition();
                gameState = GameStates.running;
            }
        }

    }

    public float cowerTime;
    public float startCowerTime = 5f;
    void Cower()
    {
        if (isPlayerChasable == false) return;
        if (cowerTime > 0)
        {
            agent.speed = 0;
            cowerTime -= 1 * Time.deltaTime;
            movementAnimator.Play("Cower");
            overheadStates.OverheadCowerState();
        }
        else
        {
            movementAnimator.SetTrigger("backToMove");
            movementAnimator.SetFloat("Move", 1f);
            agent.speed = 1;
            cowerTime = startCowerTime;
            gameState = GameStates.patroling;
        }
    }

    void StartWander()
    {
        overheadStates.HideStateOverheads();
        movementAnimator.SetFloat("Move", Mathf.Clamp(agent.velocity.sqrMagnitude, 0.1f, 1.2f));
        pedestrian.GetComponent<PedestrianWander>().enabled = true;
    }
    void StopWander()
    {
        pedestrian.GetComponent<PedestrianWander>().enabled = false;
    }

}
