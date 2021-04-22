using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PauseController pauseController;
    [SerializeField] private Selector leftSelector;
    [SerializeField] private Selector rightSelector;
    [SerializeField] private Slider leftUpgradeMeter;
    [SerializeField] private Slider rightUpgradeMeter;
    [SerializeField] private Image leftArrow;
    [SerializeField] private Image rightArrow;

    [Header("Settings")]
    [SerializeField] private int leftUpgradeThreshold;
    [SerializeField] private int rightUpgradeThreshold;

    private int leftUpgrade = 0;
    private int rightUpgrade = 0;

    // Constants
    private Vector3 pivot = new Vector3(0f, 2.45f, -5f);


    private void Start() 
    {
        UpdateUpgradeMeters();
    }

    // Invoked by Units when they get past the enemy line
    public void Point(int team, float value)
    {
        // Adjust score display
        float newXScale = team * value + transform.localScale.x;
        ScaleAround(new Vector3(newXScale, transform.localScale.y, transform.localScale.z));
        
        // Detect win
        if (newXScale > 449 || newXScale < -449)
        {
            pauseController.WinGame(team);
            return;
        }
        
        // Count towards upgrades
        if (team == -1 && leftUpgrade < leftUpgradeThreshold)
        {
            leftUpgrade += 1;
            if (leftUpgrade >= leftUpgradeThreshold)
            {
                // make it harder for right to upgrade
                rightUpgradeThreshold += 1;

                // let left upgrade a unit
                leftSelector.EnableUpgrade();
            }
        }
        else if (team == 1 && rightUpgrade < rightUpgradeThreshold)
        {
            rightUpgrade += 1;
            if (rightUpgrade >= rightUpgradeThreshold)
            {
                // make it harder for left to upgrade
                leftUpgradeThreshold += 1;

                // let right upgrade a unit
                rightSelector.EnableUpgrade();
            }
        }

        // Update Upgrade Display
        UpdateUpgradeMeters();
    }

    private void UpdateUpgradeMeters() 
    {
        leftUpgradeMeter.value = leftUpgrade;
        leftUpgradeMeter.maxValue = leftUpgradeThreshold;
        if (leftUpgrade >= leftUpgradeThreshold)
            leftArrow.color = Options.LEFT_COLOR;
        else
            leftArrow.color = Options.TRANSPARENT;

        rightUpgradeMeter.value = rightUpgrade;
        rightUpgradeMeter.maxValue = rightUpgradeThreshold;
        if (rightUpgrade >= rightUpgradeThreshold)
            rightArrow.color = Options.RIGHT_COLOR;
        else
            rightArrow.color = Options.TRANSPARENT;
    }

    public void ResetUpgradeMeter(int team) 
    {
        if (team == 1)
            leftUpgrade = 0;
        else
            rightUpgrade = 0;
        UpdateUpgradeMeters();
    }

    // credit: KyleBlumreisinger on Unity Forums
    // https://forum.unity.com/threads/scale-around-point-similar-to-rotate-around.232768/
    private void ScaleAround(Vector3 newScale)
    {
        Vector3 A = transform.localPosition;
        Vector3 B = pivot;
    
        Vector3 C = A - B; // diff from object pivot to desired pivot/origin
    
        float RS;
        if (transform.localScale.x == 0)
            RS = newScale.x;
        else
            RS = newScale.x / transform.localScale.x; // relative scale factor

        // calc final position post-scale
        Vector3 FP = B + C * RS;
    
        // finally, actually perform the scale/translation
        transform.localScale = newScale;
        transform.localPosition = FP;
    }
}
