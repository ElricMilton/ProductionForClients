using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : MonoBehaviour
{
    AudioSource streetChatter;
    GameObject player;
    GameObject burstFX;
    //public IntVariable powerBarSpeed;

    bool volumeDown = false;
    bool volumeUp = false;
    float timeStartedLerping;
    float lerpTime = 1.5f;
    float startValue = 0.9f;
    float endValue = 0.3f;

    private void Start()
    {
        streetChatter = GameObject.FindGameObjectWithTag("StreetChatterAudio").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<MeshRenderer>().enabled = false;
        burstFX = GameObject.Find("Burst VFX");
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
            burstFX.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //powerBarSpeed.Value = 0;
            volumeDown = true;
            volumeUp = false;
            StartLerping();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            burstFX.transform.localScale = new Vector3(1f, 1f, 1f);
            //powerBarSpeed.Value = 5;
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
