    using System;
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float theSpeed = 7;
    public static float currentSpeed = 7;
    public static bool isSpeedTriggered = false;
    public static float speedBoostTimer = 0;
    
    public static float maxHealth = 100;
    public static float currentHealth = 100;

    public static float score = 0;

    public static float inGameStarDust = 0;
    
    public static float maxEnergy = 100;
    public static float currentEnergy = 100;

    private static bool isGameBeginned = false;



    //SETUP FUNCTION
    void Start()
    {

    }

    //MAIN FUNCTIONS
    void Update()
    {
        GameStart();
        Movement();
        Score();
        TriggerEvents();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        SpeedBoost(collider);
    }
    
    private void OnCollisionEnter2D(Collision2D collider)
    {
        Obstacle(collider);
    }

    //UPDATE FUNCTIONS
    private void GameStart()
    {
        if (!isGameBeginned && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            isGameBeginned = true;
        }
    }
    private void Movement()
    {

        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * currentSpeed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);
        
        float inputY = Input.GetAxisRaw("Vertical");
        float velocityY = inputY * currentSpeed;
        transform.Translate(Vector2.up * velocityY * Time.deltaTime);
    }
    
    private void Score()
    {
        if (isGameBeginned)
        {
            score += 0.20f;
            Debug.Log("score = " + String.Format("{0:0.00}", score));
        }
       
    }
    
    private void TriggerEvents()
    {
        //speed
        if (isSpeedTriggered)
        {
            float eventsDuration = 1.5f;
            speedBoostTimer += Time.deltaTime;
            if (speedBoostTimer >= eventsDuration)
            {
                currentSpeed = theSpeed;
                speedBoostTimer = 0;
                isSpeedTriggered = false;
            }
        }
        
        
    }

    //ON TRIGGER FUNCTIONS
    private void SpeedBoost(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("SpeedBoost"))
        {
            Debug.Log(currentHealth);
            isSpeedTriggered = true;
            IncreaseSpeed(2);
            Destroy(collider.gameObject);
        }
    }
    
    //ON COLLISION FUNCTIONS
    private void Obstacle(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            DecreaseHealth(10);
            Debug.Log(currentHealth);
        }
    }
    
    //IN GAME POWER UPS FUNCTIONS
    private void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    
    private void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    private void IncreaseSpeed(int ratio)
    {
        currentSpeed *= ratio;
    }
    
   
}
