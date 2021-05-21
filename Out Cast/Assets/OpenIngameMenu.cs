using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpenIngameMenu : MonoBehaviour
{
    private GameObject menuContainer;

    public CinemachineBrain CMBrain;

    // Start is called before the first frame update
    void Start()
    {
        menuContainer = this.transform.Find("MenuContainer").gameObject;
        menuContainer.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuContainer.activeSelf == false)
        {
            menuContainer.SetActive(true);
            Time.timeScale = 0.0f;
            CMBrain.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuContainer.activeSelf == true)
        {
            menuContainer.SetActive(false);
            Time.timeScale = 1.0f;
            CMBrain.enabled = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            CMBrain.enabled = true;
        }
    }
}
