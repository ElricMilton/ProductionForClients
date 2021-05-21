using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWallController : MonoBehaviour
{
    Animator anim;
    GameObject player;
    public ParticleSystem particles;
    AudioSource audio;

    bool hasBeenTriggered = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == player || other.gameObject.CompareTag("Cop")) && hasBeenTriggered == false)
        {
            anim.Play("StartWallSpawn");
            particles.Play();
            audio.Play();
            hasBeenTriggered = true;
        }
    }
}
