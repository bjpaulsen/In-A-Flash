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
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    [Header("Settings")]
    [SerializeField] private int leftUpgradeThreshold;
    [SerializeField] private int rightUpgradeThreshold;

    private int leftUpgradeProgress = 0;
    private int rightUpgradeProgress = 0;

    private int leftUpgradeTotal = 0;
    private int rightUpgradeTotal = 0;

    private static int numUnits = 3;

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
        if (team == -1 && leftUpgradeTotal < numUnits && leftUpgradeProgress < leftUpgradeThreshold)
        {
            leftUpgradeProgress += 1;
            if (leftUpgradeProgress >= leftUpgradeThreshold)
            {
                // Record upgrade
                leftUpgradeTotal += 1;

                // Make it harder for right to upgrade unless they have an upgrade ready
                if (rightUpgradeProgress < rightUpgradeThreshold)
                    rightUpgradeThreshold += 1;

                // Let left upgrade a unit
                leftSelector.EnableUpgrade();
            }
        }
        else if (team == 1 && rightUpgradeTotal < numUnits && rightUpgradeProgress < rightUpgradeThreshold)
        {
            rightUpgradeProgress += 1;
            if (rightUpgradeProgress >= rightUpgradeThreshold)
            {
                // Record upgrade
                rightUpgradeTotal += 1;

                // Make it harder for left to upgrade unless they have an upgrade ready
                if (leftUpgradeProgress < leftUpgradeThreshold)
                    leftUpgradeThreshold += 1;

                // Let right upgrade a unit
                rightSelector.EnableUpgrade();
            }
        }

        // Update Upgrade Display
        UpdateUpgradeMeters();
    }

    private void UpdateUpgradeMeters() 
    {
        leftUpgradeMeter.value = leftUpgradeProgress;
        leftUpgradeMeter.maxValue = leftUpgradeThreshold;
        leftArrow.SetActive(leftUpgradeProgress >= leftUpgradeThreshold);

        rightUpgradeMeter.value = rightUpgradeProgress;
        rightUpgradeMeter.maxValue = rightUpgradeThreshold;
        rightArrow.SetActive(rightUpgradeProgress >= rightUpgradeThreshold);
    }

    public void ResetUpgradeMeter(int team) 
    {
        if (team == 1)
            leftUpgradeProgress = 0;
        else
            rightUpgradeProgress = 0;
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
