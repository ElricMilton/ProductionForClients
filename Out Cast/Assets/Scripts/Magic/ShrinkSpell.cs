using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkSpell : MonoBehaviour
{
    [SerializeField] float cooldown = 3;
    [SerializeField] GameEvent shrinkSpellEvent;
    Animator anim;
    AudioManager audioManager;

    bool castable = true;
    bool shrinking = false;
    bool delayGrowth = false;
    bool hasPressed = false;
    bool fullSize = true;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && hasPressed == false)
        {
            hasPressed = true;
            shrinking = true;
            Shrink();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            hasPressed = false;
            Grow();
        }
        if (!Input.GetKey(KeyCode.LeftShift) && !fullSize)
        {
            Grow();
        }
    }

    void Shrink()
    {
        if (castable)
        {
            fullSize = false;
            anim.Play("Shrink");
            audioManager.Play("Shrink");
            StartCoroutine(ShrinkTime());
        }
    }
    void Grow()
    {
        if (castable)
        {
            if (!shrinking)
            {
                castable = false;
                audioManager.Play("Grow");
                anim.Play("Grow");
                StartCoroutine(Cooldown());
                fullSize = true;
            }
        }
        else if (castable)
        {
            if (shrinking)
            {
                castable = false;
                delayGrowth = true;
                StartCoroutine(GrowAfterX());
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        castable = true;
    }

    IEnumerator ShrinkTime()
    {
        yield return new WaitForSeconds(0.45f);
        shrinkSpellEvent.Invoke();
        shrinking = false;
    }

    IEnumerator GrowAfterX()
    {
        yield return new WaitForSeconds(0.45f);
        fullSize = true;
        audioManager.Play("Grow");
        anim.Play("Grow");
        StartCoroutine(Cooldown());
        delayGrowth = false;
        hasPressed = false;

    }
}
