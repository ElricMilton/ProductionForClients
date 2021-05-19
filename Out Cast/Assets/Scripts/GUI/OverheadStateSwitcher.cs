using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadStateSwitcher : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject alertImage;
    [SerializeField] GameObject cowerImage;
    [SerializeField] GameObject searchingImage;
    [SerializeField] GameObject chasingImage;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void OverheadAlertState()
    {
        alertImage.SetActive(true);
        cowerImage.SetActive(false);
        searchingImage.SetActive(false);
        chasingImage.SetActive(false);
        anim.Play("DialogueSpawn");
    }
    public void OverheadCowerState()
    {
        cowerImage.SetActive(true);
        alertImage.SetActive(false);
        searchingImage.SetActive(false);
        chasingImage.SetActive(false);
        anim.Play("DialogueSpawn");
    }
    public void OverheadSearchingState()
    {
        searchingImage.SetActive(true);
        alertImage.SetActive(false);
        cowerImage.SetActive(false);
        chasingImage.SetActive(false);
        anim.Play("DialogueSpawn");
    }
    public void OverheadChasingState()
    {
        chasingImage.SetActive(true);
        alertImage.SetActive(false);
        cowerImage.SetActive(false);
        searchingImage.SetActive(false);
        anim.Play("DialogueSpawn");
    }
    public void HideStateOverheads()
    {
        anim.Play("DialogueDespawn");
    }

    IEnumerator PlayAnimAfterX()
    {
        anim.Play("DialogueDespawn");
        yield return new WaitForSeconds(0.2f);
        alertImage.SetActive(false);
        cowerImage.SetActive(false);
        searchingImage.SetActive(false);
        chasingImage.SetActive(false);
    }
}
