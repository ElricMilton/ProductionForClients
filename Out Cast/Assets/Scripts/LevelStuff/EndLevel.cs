using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] GameEvent endLevelEvent;
    GameObject player;
    [SerializeField] Animator endWallAnim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            endWallAnim.Play("SafeZoneReveal");
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {

        yield return new WaitForSeconds(3);
        endLevelEvent.Invoke();

    }
}
