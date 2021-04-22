using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDisplay : MonoBehaviour
{
    public GameObject unitSelector;
    public int selectedUnit = 0;
    public int numUnits;

    private SpriteRenderer[] renderers;

    void Start()
    {
        // Get references to the SpriteRenderers of each cooldown display icon
        renderers = new SpriteRenderer[numUnits];
        int index = 0;
        // NOTE: has 1 extra transform for the selector at the end
        foreach (Transform childTransform in transform)
        {
            if (index < numUnits)
                renderers[index] = childTransform.gameObject.GetComponent<SpriteRenderer>();
            index++;
        }
    }

    public void UpdateColor(int whichUnit, Unit unit, int cooldown)
    {
        SpriteRenderer sr = renderers[whichUnit];
        float alpha = (float)cooldown / unit.TotalCooldown;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    public void MoveUnitSelector(int direction, int team)
    {
        // reverse direction if on right side
        direction *= team == 1? 1 : -1;

        // too far off the beginning
        if (selectedUnit + direction < 0)
            selectedUnit = numUnits - 1;
        // too far off the end
        else if (selectedUnit + direction >= numUnits)
            selectedUnit = 0;
        // within bounds
        else
            selectedUnit += direction;

        // move selector
        Select(selectedUnit, team);
    }

    public void Select(int unit, int team)
    {
        selectedUnit = unit;

        // set selector position
        Transform sel = unitSelector.transform;
        sel.position = new Vector3((-8 * team) + selectedUnit * (.5f * team), sel.position.y, sel.position.z);
    }
}
