using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianCharacterSwap : MonoBehaviour
{
    public int characterValue = 0;

    public GameObject character0;
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public GameObject character4;
    public GameObject character5;

    void Start()
    {
        characterValue = Random.Range(0, 5);
    }

    private void Update()
    {

        if (characterValue == 0)
        {
            character0.SetActive(true);
            character1.SetActive(false);
            character2.SetActive(false);
            character3.SetActive(false);
            character4.SetActive(false);
            character5.SetActive(false);
        }
        else if (characterValue == 1)
        {
            character0.SetActive(false);
            character1.SetActive(true);
            character2.SetActive(false);
            character3.SetActive(false);
            character4.SetActive(false);
            character5.SetActive(false);
        }
        else if (characterValue == 2)
        {
            character0.SetActive(false);
            character1.SetActive(false);
            character2.SetActive(true);
            character3.SetActive(false);
            character4.SetActive(false);
            character5.SetActive(false);
        }
        else if (characterValue == 3)
        {
            character0.SetActive(false);
            character1.SetActive(false);
            character2.SetActive(false);
            character3.SetActive(true);
            character4.SetActive(false);
            character5.SetActive(false);
        }
        else if (characterValue == 4)
        {
            character0.SetActive(false);
            character1.SetActive(false);
            character2.SetActive(false);
            character3.SetActive(false);
            character4.SetActive(true);
            character5.SetActive(false);
        }
        else if (characterValue == 5)
        {
            character0.SetActive(false);
            character1.SetActive(false);
            character2.SetActive(false);
            character3.SetActive(false);
            character4.SetActive(false);
            character5.SetActive(true);
        }
    }
}
