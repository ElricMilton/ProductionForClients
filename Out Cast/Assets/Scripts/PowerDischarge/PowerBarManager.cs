using UnityEngine;
using UnityEngine.UI;

public class PowerBarManager : MonoBehaviour
{
    //Power bar info:
    public Slider slider;
    public Image fill;
    public Gradient colourOverLife; //Set fixed colour gradient
    public float maxPower = 100;
    public float currentPower;
    public float powerIncreaseSpeed;
    [SerializeField] GameEvent powerDischarge;

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
    }

    void GainPowerOverTime()
    {
        IncreasePower(powerIncreaseSpeed);
    }
}
