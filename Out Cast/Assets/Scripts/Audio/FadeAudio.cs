using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
    AudioSource streetChatter;
    GameObject player;

    bool volumeDown = false;
    bool volumeUp = false;
    float timeStartedLerping;
    float lerpTime = 1.5f;
    float startValue = 0.9f;
    float endValue = 0.3f;

    private void OnEnable()
    {
        streetChatter = GameObject.FindGameObjectWithTag("StreetChatterAudio").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update()
    {
        if (volumeDown)
        {
            streetChatter.volume = Lerp(startValue, endValue, timeStartedLerping, lerpTime);
        }
        else if (volumeUp)
        {
            streetChatter.volume = Lerp(endValue, startValue, timeStartedLerping, lerpTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            volumeDown = true;
            volumeUp = false;
            StartLerping();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            volumeUp = true;
            volumeDown = false;
            StartLerping();
        }
    }

    void StartLerping()
    {
        timeStartedLerping = Time.time;        
    }

    public float Lerp(float start, float end, float timeStartedLerping, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var result = Mathf.Lerp(start, end, percentageComplete);

        return result;
    }
}
