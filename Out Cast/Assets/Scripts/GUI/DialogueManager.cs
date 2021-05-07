using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject popup;
    [SerializeField] Animator anim;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //StopCoroutine(DeactivateAfterX());
            popup.SetActive(true);
            anim.Play("DialogueSpawn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            anim.Play("DialogueDespawn");
            StartCoroutine(DeactivateAfterX());
        }
    }

    IEnumerator DeactivateAfterX()
    {
        yield return new WaitForSeconds(0.25f);
        popup.SetActive(false);

    }

}
