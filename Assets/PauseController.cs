using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject leftVictory;
    [SerializeField] private GameObject rightVictory;
    [HideInInspector] public static bool paused = false;
    private static bool gameActive = true;

    void Update()
    {
        if (Input.GetButtonDown("Pause") && gameActive) 
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
        menuCanvas.SetActive(true);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Unpause()
    {
        paused = false;
        gameActive = true;
        Time.timeScale = 1;
        menuCanvas.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void WinGame(int team)
    {
        if (!gameActive)
            return;
        
        gameActive = false;
        paused = true;
        Time.timeScale = 0;
        menuCanvas.SetActive(true);

        if (team == 1)
            leftVictory.SetActive(true);
        else
            rightVictory.SetActive(true);
    }

    public void BeginTutorial() 
    {
        gameActive = false;
    }

    public void EndTutorial() 
    {
        paused = false;
        gameActive = true;
        Time.timeScale = 1;
    }
}
