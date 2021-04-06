using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private Color color;
    public Color Color { get; }
    
    [SerializeField] private int team;
    public int Team { get; }

    public Unit[] units;
    public int[] cooldowns;

    public CooldownDisplay cooldownDisplay;

    public float laneDistance = 1.375f;
    public float spawnPosition = -9.45f;
    private int maxLane = 2;
    private int minLane = -4;

    void Awake()
    {
        // Set up cooldowns
        cooldowns = new int[units.Length];
        for (int i = 0; i < cooldowns.Length; i++)
        {
            cooldowns[i] = units[i].TotalCooldown;
            Debug.Log(cooldowns[i]);
        }
    }

    void FixedUpdate() {
        // Update cooldowns
        for (int i = 0; i < cooldowns.Length; i++)
            if (cooldowns[i] < units[i].TotalCooldown)
                cooldowns[i] += 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Move the lane selector
        if (Input.GetButtonDown(name + "Up") && transform.position.y < maxLane)
			Move(laneDistance);
        if (Input.GetButtonDown(name + "Down") && transform.position.y > minLane)
			Move(-laneDistance);

        // Move the unit selector
        if (Input.GetButtonDown(name + "Left"))
			cooldownDisplay.MoveUnitSelector(-1);
        if (Input.GetButtonDown(name + "Right"))
			cooldownDisplay.MoveUnitSelector(1);

        // Spawn units
        int selected = cooldownDisplay.selectedUnit;
        if (Input.GetButton(name + "Spawn") && cooldowns[selected] == units[selected].TotalCooldown)
            Spawn(units[selected]);
    }

    void Move(float moveDistance)
    {
        transform.position = new Vector3(transform.position.x, 
                                         transform.position.y + moveDistance, 
                                         transform.position.z);
    }

    void Spawn(Unit unitPrefab)
    {
        // Spawn the unit
        Unit unit = Instantiate(unitPrefab, 
                    new Vector3(spawnPosition, transform.position.y, transform.position.z),
                    transform.rotation);
        
        // Set team, travel direction, color
        unit.Team = team;
        SpriteRenderer sr = unit.GetComponent<SpriteRenderer>();
        sr.color = Color;

        // Reset cooldowns
        for (int i = 0; i < cooldowns.Length; i++)
            cooldowns[i] = 0;
    }
}
