using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpenIngameMenu : MonoBehaviour
{
    private GameObject menuContainer;

    public bool menuLock;
    public CinemachineFreeLook CMCam;

    // Start is called before the first frame update
    void Start()
    {
        menuContainer = this.transform.Find("MenuContainer").gameObject;
        menuContainer.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuLock)
        {
            menuContainer.SetActive(true);
            menuLock = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuLock)
        {
            menuLock = false;
            Cursor.lockState = CursorLockMode.Locked;
            menuContainer.SetActive(false);
        }
        
        if (menuLock)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            CMCam.m_XAxis.m_MaxSpeed = 0;
            CMCam.m_YAxis.m_MaxSpeed = 0;
        }
        else if (!menuLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            CMCam.m_XAxis.m_MaxSpeed = 450;
            CMCam.m_YAxis.m_MaxSpeed = 4;
        }
    }

    public void menuLockToggle()
    {
        menuLock = false;
    }
}
