using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [HideInInspector] public static bool paused = false;

    void Update()
    {
        if (Input.GetButtonDown("Pause")) 
        {
            if (paused)
                Unpause();
            else
                Pause();
        } 
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
