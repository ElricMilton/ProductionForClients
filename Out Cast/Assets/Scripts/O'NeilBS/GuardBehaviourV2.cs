using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SensorToolkit;

public class GuardBehaviourV2 : MonoBehaviour
{

    public BoolVariable chaseSatus;
    
    public Animator movementAnimator;
    public PlayerStatus playerPos;
    public NavMeshAgent agent;

    public GameObject guard;

    // public RangeSensor sensor;
    public TriggerSensor fov;
    public Waypoint post;
    public float chaseSpeed = 4f;

    //for searching state
    public float speed;
    public float timerDecrease = 1f;
    public GameObject patrolPoint;

    //search timer vars
    public float searchTime;
    public float startSearchTime;

    public AudioClip alertedClip;
    public AudioClip returningToPostClip;
    public AudioClip idleChatterClip1;
    public AudioClip idleChatterClip2;
    AudioSource audioSource;
    bool alertPlaying = false;
    bool returnPlaying = false;
    bool idle1Playing = false;
    bool idle2Playing = false;

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
        audioSource = GetComponent<AudioSource>();
        StartPatrol();
        gameState = GameStates.patroling;
        searchTime = startSearchTime;
    }

    void CheckDistance()
    { 
        var player = fov.GetNearest(); 
    }

    void Update()
    {


        switch (gameState)
        {
            case GameStates.patroling:
                Debug.Log("We are in state patroling!");
                alertPlaying = false;
                returnPlaying = false;
                idle2Playing = false;

                if (!idle1Playing)
                {
                    audioSource.PlayOneShot(idleChatterClip1);
                    idle1Playing = true;
                }

                StartPatrol();
                break;
            case GameStates.chasing:
                Debug.Log("We are in state chasing!");
               // returnPlaying = false;
               // idle2Playing = false;
               // idle1Playing = false;

               // if (!alertPlaying)
                {
                    audioSource.PlayOneShot(alertedClip);
                    alertPlaying = true;
                }
                //Chasing();
                //StopPatrol();
                break;
            case GameStates.searching:
                //Debug.Log("We are in state searching!");
                IsSearching();
                break;
            case GameStates.returningToPost:
                Debug.Log("We are in state returningToPost!");
                idle2Playing = false;
                idle1Playing = false;
                alertPlaying = false;

                if (!returnPlaying)
                {
                    audioSource.PlayOneShot(returningToPostClip);
                    returnPlaying = true;
                }
                ReturnToPost();
                break;
            default:
                break;
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    gameState = GameStates.patroling;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    gameState = GameStates.chasing;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    gameState = GameStates.searching;
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    gameState = GameStates.returningToPost;
        //}



        var player = fov.GetNearest();
        if (player != null & chaseSatus.Value == true)
        {
            searchTime = startSearchTime;
            gameState = GameStates.chasing;

        }

        if (gameState == GameStates.chasing & player == null)
        {
            gameState = GameStates.searching;
        }


    }

    public void Chasing()
    {
        audioSource.PlayOneShot(alertedClip);
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
            movementAnimator.SetFloat("Move", 1f);
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
            movementAnimator.SetFloat("Move", 0.5f);
        }
        else
        {
            gameState = GameStates.patroling;
        }
    }

    void IsSearching()
    {
        if ((transform.position - playerPos.playerLastPos).magnitude > 0.5f)
        {
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
            movementAnimator.Play("Searching");
            searchTime -= (timerDecrease * Time.deltaTime);
        }
        else
        {
            movementAnimator.Play("Movement");
            gameState = GameStates.returningToPost;
            searchTime = startSearchTime;
        }
    }

    void StartPatrol()
    {
        movementAnimator.SetFloat("Move", 0.5f);
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
