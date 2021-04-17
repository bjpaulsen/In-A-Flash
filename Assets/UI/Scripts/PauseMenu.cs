using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References to Components")]
    [SerializeField] private PauseController pauseController;

    public void Resume() 
    {
        pauseController.Unpause();
    }

    public void Quit() 
    {
        pauseController.Unpause();
        SceneManager.LoadScene("MainMenuScene");
    }
}
