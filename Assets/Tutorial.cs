using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("References to Components")]
    [SerializeField] private PauseController pauseController;
    [SerializeField] private Score score;

    // Start is called before the first frame update
    void Start()
    {
        pauseController.BeginTutorial();
    }

    public void Quit()
    {
        // Reset scene and move to main menu
        pauseController.EndTutorial();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void EnableUpgrades()
    {
        // Earn both sides upgrades
        for (int i = 0; i < 10; i++)
        {
            score.Point(1, 1f);
            score.Point(-1, 1f);
        }
    }

    public void WinTutorial()
    {
        // Fill score bar
        score.Point(1, 1800f);

        // Shake camera
        Camera.main.GetComponent<CameraShake>().ShakeCamera();
    }

    public void UnWinTutorial()
    {
        // Un-fill score bar
        score.Point(-1, 1800f);
    }
}
