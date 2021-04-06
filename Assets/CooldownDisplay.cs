using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDisplay : MonoBehaviour
{
    public Selector selector;
    public GameObject unitSelector;
    public int selectedUnit = 0;
    public int numUnits;

    private SpriteRenderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        // Update colors of display
        for (int i = 0; i < numUnits; i++)
            UpdateColor(renderers[i], selector.units[i], selector.cooldowns[i]);
    }

    private void UpdateColor(SpriteRenderer sr, Unit unit, int cooldown)
    {
        float alpha = (float)cooldown / unit.TotalCooldown;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    public void MoveUnitSelector(int direction)
    {
        // reverse direction if on right side
        direction *= selector.Team == 1? 1 : -1;

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
        Select(selectedUnit);
    }

    public void Select(int unit)
    {
        selectedUnit = unit;

        // set selector position
        Transform sel = unitSelector.transform;
        sel.position = new Vector3((-8 * selector.Team) + selectedUnit * (.5f * selector.Team), sel.position.y, sel.position.z);
    }
}
