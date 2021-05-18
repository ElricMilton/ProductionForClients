﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarManager : MonoBehaviour
{
    //Power bar info:
    [SerializeField] Slider slider;
    [SerializeField] Image fill;
    [SerializeField] Gradient colourOverLife; //Set fixed colour gradient
    [SerializeField] int maxPower = 100;
    [SerializeField] int currentPower;
    [SerializeField] int powerIncreaseSpeed;
    [SerializeField] GameEvent powerDischarge;
    [SerializeField] int howManySecondsToDischargeFor;
    [SerializeField] BoolVariable isDischargingMagicBool;
    [SerializeField] BoolVariable isPlayerChasable;
    [SerializeField] ParticleSystem uiDischargeFX;

    public void SetMaxPower(int power)
    {
        slider.maxValue = power;
        //slider.value = power;
        fill.color = colourOverLife.Evaluate(1);
    }

    public void SetPower(int power)
    {
        slider.value = power;
        fill.color = colourOverLife.Evaluate(slider.normalizedValue);
    }

    void Start()
    {
        isPlayerChasable.Value = false;
        SetPower(0);
        SetMaxPower(maxPower);
        InvokeRepeating("GainPowerOverTime", 0.1f, 0.1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpendPower(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            IncreasePower(5);
        }
    }

    public void SpendPower(int powerAmount)
    {
        currentPower -= powerAmount;
        SetPower(currentPower);
        if (currentPower <= 0)
        {
            currentPower = 0;
        }
        ReduceFX();
    }

    void ReduceFX()
    {
        //play effects here.
    }

    //Call this function to trigger power increase
    public void IncreasePower(int powerAmount)
    {
        currentPower += powerAmount;
        SetPower(currentPower);
        if (currentPower >= maxPower)
        {
            currentPower = 0;
            OnBurst();
        }
        IncreaseFX();
    }

    void IncreaseFX()
    {
        //Animate power bar or play effect here.
    }

    void OnBurst()
    {
        uiDischargeFX.Play();
        StartCoroutine(DischargingMagic());
        powerDischarge?.Invoke();
    }

    void GainPowerOverTime()
    {
        IncreasePower(powerIncreaseSpeed);
    }

    IEnumerator DischargingMagic()
    {
        isPlayerChasable.Value = true;
        uiDischargeFX.gameObject.SetActive(true);
        isDischargingMagicBool.Value = true;
        yield return new WaitForSeconds(howManySecondsToDischargeFor);
        uiDischargeFX.gameObject.SetActive(false);
        isDischargingMagicBool.Value = false;
        isPlayerChasable.Value = true;
    }
}
