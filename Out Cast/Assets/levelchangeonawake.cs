using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelchangeonawake : MonoBehaviour
{
    public string level;

    void Start()
    {
        SceneManager.LoadScene(level);
    }
}
