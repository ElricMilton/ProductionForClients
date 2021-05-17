using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] GameEvent endLevelEvent;
    GameObject player;
    [SerializeField] Animator endWallAnim;
    [SerializeField] AudioSource stoneSlideSFX;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            
            StartCoroutine(End());
        }
    }

    IEnumerator End()
    {
        stoneSlideSFX.Play();
        yield return new WaitForSeconds(0.2f);
        endWallAnim.Play("SafeZoneReveal");
        yield return new WaitForSeconds(4);
        endLevelEvent.Invoke();

    }
}
