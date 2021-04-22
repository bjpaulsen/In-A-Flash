using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("References To Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject particles;
    private Score score;
    protected AudioManager audioManager;

    // team represents which side the unit is on, its travel direction, color, etc
    private int team;
    public int Team
    {
        get { return team; }
        set {
            if (value == 1 || value == -1)
                team = value;
            else
                throw new System.Exception("UNIT ERROR: team set to unsafe value of " + value);
        }
    }

    [Header("Unit Attributes")]
    [SerializeField] private float value = 87;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float health = 1;
    [SerializeField] private float attackPower = 1;
    [SerializeField] private int totalCooldown;
    public int TotalCooldown { get { return totalCooldown; } }

    [Header("Pulse Settings")]
    [SerializeField] private float pulseMinScale;
    [SerializeField] private float pulseMaxScale;
    [SerializeField] private float pulsesPerSecond;

    // Bounds-checking data
    private float startingXScale;
    private float startingYScale;
    private float goal;
    private static float screenTop = 5;
    private static float screenBottom = -5;

    public void Start()
    {
        // Get reference to Audio Manager
        audioManager = FindObjectOfType<AudioManager>();
        
        // Get reference to Score
        GameObject scoreSprite = GameObject.Find("Score");
        if (scoreSprite != null)
            score = scoreSprite.GetComponent<Score>();
        else
            throw new System.Exception("UNIT ERROR: Missing Score Gameobject");

        // Remember scale for pulse animation
        startingXScale = transform.localScale.x;
        startingYScale = transform.localScale.y;

        // Set up goal checking
        goal = transform.position.x * -1;
    }

    public virtual void Update()
    {
        float scale = Mathf.SmoothStep(pulseMinScale, pulseMaxScale, (1+Mathf.Sin(2*Mathf.PI*Time.time*pulsesPerSecond))/2);
        transform.localScale = new Vector3(startingXScale*scale, startingYScale*scale, transform.localScale.z);
    }

    void FixedUpdate()
    {
        Move();
        CheckGoal();
        CheckSides();
    }

    public void Move()
    {
        // rb.position = new Vector2(rb.position.x + (moveSpeed * Time.deltaTime * team), 
        //                                  rb.position.y);
        Vector3 force = team == 1? Vector3.right*moveSpeed : Vector3.left*moveSpeed;
        rb.AddForce(force);
    }

    public void CheckGoal()
    {
        if (team == 1)
        {
            if (transform.position.x > goal)
            {
                score.Point(team, value);
                ReadyDeath();
            }
        }
        else
        {
            if (transform.position.x < goal)
            {
                score.Point(team, value);
                ReadyDeath();
            }
        }
    }

    public void CheckSides()
    {
        if (transform.position.y > screenTop || transform.position.y < screenBottom)
            ReadyDeath();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= .1f)
        {
            ReadyDeath();
        }
    }

    public virtual void ReadyDeath()
    {
        Done();
    }

    public void Done()
    {
        // Shake camera
        Camera.main.GetComponent<CameraShake>().ShakeCamera();

        // Spawn particle effect
        GameObject p = Instantiate(particles, transform.position, transform.rotation);
        Vector3 particleVelocity = new Vector3(rb.velocity.x*0.5f, rb.velocity.y, 0);
        p.GetComponent<Rigidbody2D>().AddForce(particleVelocity, ForceMode2D.Impulse);
        // adjust particle color
        var main = p.GetComponent<ParticleSystem>().main;
        main.startColor = GetComponent<SpriteRenderer>().color;

        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Unit>() && other.collider.GetComponent<Unit>().Team != team)
        {
            Unit enemyUnit = other.collider.GetComponent<Unit>();
            enemyUnit.Damage(attackPower);
        }
    }
}
