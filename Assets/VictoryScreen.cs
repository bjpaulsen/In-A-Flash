using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [Header("References to Components")]
    [SerializeField] private PauseController pauseController;

    public void Rematch() 
    {
        pauseController.Unpause();
        SceneManager.LoadScene("PlayScene");
    }

    public void Quit() 
    {
        pauseController.Unpause();
        SceneManager.LoadScene("MainMenuScene");
    }
}
