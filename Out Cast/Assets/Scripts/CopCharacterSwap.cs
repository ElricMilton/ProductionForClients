using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopCharacterSwap : MonoBehaviour
{
    public int characterValue = 0;

    public GameObject character0;
    public GameObject character1;

    void Start()
    {
        characterValue = Random.Range(0, 2);
    }

    private void Update()
    {

        if (characterValue == 0)
        {
            character0.SetActive(true);
            character1.SetActive(false);
        }
        else if (characterValue == 1)
        {
            character0.SetActive(false);
            character1.SetActive(true);
        }
    }
}
