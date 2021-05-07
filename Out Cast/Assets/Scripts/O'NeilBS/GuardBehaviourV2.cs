using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class GuardBehaviourV2 : MonoBehaviour
{

    public BoolVariable chaseSatus;
    public Animator guardStateMachineAnimator;
    public Animator movementAnimator;
    public PlayerStatus playerPos;
    public NavMeshAgent agent;

    public GameObject guard;

    // public RangeSensor sensor;
    public TriggerSensor fov;
    public GameObject post;
    public float chaseSpeed = 4f;

    //for searching state
    public float speed;
    public float timerDecrease = 1f;
    public GameObject patrolPoint;

    //search timer vars
    public float searchTime;
    public float startSearchTime;

    
    public enum GameStates
    {
        patroling,
        chasing,
        searching,
        returningToPost,

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
            case GameStates.chasing:
                Debug.Log("We are in state chasing!");
                Chasing();
                StopPatrol();
                break;
            case GameStates.searching:
                Debug.Log("We are in state searching!");
                IsSearching();
                break;
            case GameStates.returningToPost:
                Debug.Log("We are in state returningToPost!");
                ReturnToPost();
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
            gameState = GameStates.chasing;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameState = GameStates.searching;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gameState = GameStates.returningToPost;
        }



        var player = fov.GetNearest();
        if (player != null & chaseSatus.Value == true)
        {
            searchTime = startSearchTime;
            gameState = GameStates.chasing;
            guardStateMachineAnimator.SetBool("IsSearching", false);
        }

        if (gameState == GameStates.chasing & player == null)
        {
            gameState = GameStates.searching;
        }


        movementAnimator.SetFloat("Move", agent.velocity.magnitude);
    }

    void Chasing()
    {
        guardStateMachineAnimator.enabled = false;
        guardStateMachineAnimator.SetBool("IsSearching", false);
        var deteced = fov.GetNearest();
        if (deteced != null)
        {
            Chase(deteced);
        }
    }
   
    void Chase(GameObject target)
    {
        playerPos.playerLastPos = target.transform.position;
        transform.LookAt(target.transform.position);
        if ((transform.position - target.transform.position).magnitude > 2f)
        {
            //transform.position += transform.forward * speed * Time.deltaTime;
            agent.SetDestination(target.transform.position);
        }
        else
        {
            return;
        }
    }

    void ReturnToPost()
    {

        if ((transform.position - post.transform.position).magnitude > 5f)
        {
            //var speed = 4f;

            //transform.LookAt(post.transform, Vector3.up);
            agent.SetDestination(post.transform.position);
            //transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            gameState = GameStates.patroling;
        }

    }

    void IsSearching()
    {
        if ((transform.position - playerPos.playerLastPos).magnitude > .5f)
        {
            var speed = 4f;

            transform.LookAt(playerPos.playerLastPos, Vector3.up);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            Search();
        }

    }

    void Search()
    {
        if (searchTime >= 0)
        {
            guardStateMachineAnimator.enabled = true;
            guardStateMachineAnimator.SetBool("IsSearching", true);
            searchTime -= (timerDecrease * Time.deltaTime);
        }
        else
        {
            guardStateMachineAnimator.enabled = false;
            guardStateMachineAnimator.SetBool("IsSearching", false);
            gameState = GameStates.returningToPost;
            searchTime = startSearchTime;
        }
    }

    void StartPatrol()
    {
        guard.GetComponent<PedestrianNavigationController>().enabled = true;
        guard.GetComponent<WaypointNavigator>().enabled = true;
        guard.GetComponent<NavMeshAgent>().enabled = false;
    }
    void StopPatrol()
    {
        guard.GetComponent<PedestrianNavigationController>().enabled = false;
        guard.GetComponent<WaypointNavigator>().enabled = false;
        guard.GetComponent<NavMeshAgent>().enabled = true;
    }


}
