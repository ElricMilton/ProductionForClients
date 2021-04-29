using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVFXToPlayer : MonoBehaviour
{
    Transform playerTransform;
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
