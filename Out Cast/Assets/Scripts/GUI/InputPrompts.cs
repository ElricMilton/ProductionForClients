using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPrompts : MonoBehaviour
{
    [SerializeField] GameObject popup;
    [SerializeField] Animator anim;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            popup.SetActive(true);
            anim.Play("DialogueSpawn");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            anim.Play("DialogueDespawn");
        }
    }
}
