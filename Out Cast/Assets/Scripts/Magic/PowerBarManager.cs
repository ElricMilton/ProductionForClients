using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarManager : MonoBehaviour
{
    //Power bar info:
    [SerializeField] Slider slider;
    [SerializeField] Image fill;
    [SerializeField] Gradient colourOverLife; //Set fixed colour gradient
    [SerializeField] float maxPower = 100;
    [SerializeField] float currentPower;
    [SerializeField] float powerIncreaseSpeed;
    [SerializeField] GameEvent powerDischarge;
    [SerializeField] float howManySecondsToDischargeFor;
    [SerializeField] BoolVariable isDischargingMagicBool;
    [SerializeField] ParticleSystem uiDischargeFX;

    public void SetMaxPower(float power)
    {
        slider.maxValue = power;
        //slider.value = power;
        fill.color = colourOverLife.Evaluate(1);
    }

    public void SetPower(float power)
    {
        slider.value = power;
        fill.color = colourOverLife.Evaluate(slider.normalizedValue);
    }

    void Start()
    {
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

    public void SpendPower(float powerAmount)
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
    public void IncreasePower(float powerAmount)
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
        powerDischarge?.Invoke();
        uiDischargeFX.Play();
        StartCoroutine(DischargingMagic(howManySecondsToDischargeFor));
    }

    void GainPowerOverTime()
    {
        IncreasePower(powerIncreaseSpeed);
    }

    public IEnumerator DischargingMagic(float dischargeForXSeconds)
    {
        isDischargingMagicBool.Value = true;
        print("player is discharging magic!");
        yield return new WaitForSeconds(dischargeForXSeconds);
        isDischargingMagicBool.Value = false;
        print("player is no longer discharging magic");
    }
}
