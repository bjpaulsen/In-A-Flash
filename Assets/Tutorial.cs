using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("References to Components")]
    [SerializeField] private PauseController pauseController;

    // Start is called before the first frame update
    void Start()
    {
        // pauseController.BeginTutorial();
    }

    public void Quit()
    {
        pauseController.EndTutorial();
        SceneManager.LoadScene("MainMenuScene");
    }
}
