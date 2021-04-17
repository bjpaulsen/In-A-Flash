﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [Header("References to Components")]
    [SerializeField] private Color color;
    [SerializeField] private CooldownDisplay cooldownDisplay;
    
    [Header("Player Settings")]
    [SerializeField] private int team;
    [SerializeField] private Unit[] units;
    private int[] cooldowns;
    public int[] Cooldowns
    {
        get {
            // Return a copy of the array so it's protected
            int[] copy = new int[cooldowns.Length];
            for (int i = 0; i < cooldowns.Length; i++)
                copy[i] = cooldowns[i];
            return copy;
        }
    }

    private float laneDistance = 1.375f;
    private float spawnPosition;
    private int maxLane = 2;
    private int minLane = -4;

    void Awake()
    {
        // Set up cooldowns
        cooldowns = new int[units.Length];
        for (int i = 0; i < cooldowns.Length; i++)
        {
            cooldowns[i] = units[i].TotalCooldown;
        }

        // Set up spawn position
        spawnPosition = -9.75f * team;
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
        // Don't control selector if paused
        if (PauseController.paused)
            return;

        // Move the lane selector
        if (Input.GetButtonDown(name + "Up") && transform.position.y < maxLane)
			Move(laneDistance);
        if (Input.GetButtonDown(name + "Down") && transform.position.y > minLane)
			Move(-laneDistance);

        // Move the unit selector
        if (Input.GetButtonDown(name + "Left"))
			cooldownDisplay.MoveUnitSelector(-1, team);
        if (Input.GetButtonDown(name + "Right"))
			cooldownDisplay.MoveUnitSelector(1, team);

        // Spawn units
        int selected = cooldownDisplay.selectedUnit;
        if (Input.GetButton(name + "Spawn") && cooldowns[selected] == units[selected].TotalCooldown)
        {
            Spawn(units[selected]);
        }

        // Update Cooldown Display
        for (int i = 0; i < units.Length; i++)
            cooldownDisplay.UpdateColor(i, units[i], cooldowns[i]);
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
        sr.color = color;

        // Reset cooldowns
        for (int i = 0; i < cooldowns.Length; i++)
            cooldowns[i] = 0;
    }
}
