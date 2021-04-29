using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SensorToolkit;

public class GaurdBehaviour : MonoBehaviour
{
    public GameObject enemy;
    public PlayerStatus ps;
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
        if (deteced != null & ps.isChaseable == true)
        {
            enemy.GetComponent<ChaseState>().enabled = true;
            enemy.GetComponent<SearchState>().enabled = false;
            enemy.GetComponent<PatrolState>().enabled = false;
            isChasing = true;
            isSearching = false;
            isPatroling = false;
        }
        if(isChasing == true & deteced == null & isSearching == false)
        {
            enemy.GetComponent<ChaseState>().enabled = false;
            enemy.GetComponent<SearchState>().enabled = true;
            enemy.GetComponent<PatrolState>().enabled = false;
            isChasing = false;
            isSearching = true;
            isPatroling = false;

        }
        if (isChasing == true & deteced == null & isSearching == true)
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
}
