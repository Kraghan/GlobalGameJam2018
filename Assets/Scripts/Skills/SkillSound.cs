﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the sound wave, disable the main
 * character controller, change the character layer
 * @class SkillSound
 */
public class SkillSound : MonoBehaviour
{
    public float             initialSpeed;
    public Vector2           initialDirection;
    private PlayerController playerController;

    // Physics
    public float        magnitude;
    private Rigidbody2D body;
    private Vector2     velocity;
    private float amplitudeY = 1f;
    private float frequence = 100;
    private float angle = 0;
    private float xPos;

    /**
     * Called at start
     */
    void Start()
    {
        magnitude        = 1.0f;
        body             = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        xPos = 0;
    }

    /**
     * Called each update
     */
    void Update()
    {
        /*xPos = Mathf.PingPong(Time.time,2) - 1;
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float y = sin * (xPos) + cos * Mathf.Sin(xPos * frequence) * amplitudeY;
        float x = cos * (xPos) - sin * Mathf.Sin(xPos * frequence) * amplitudeY;

        body.velocity += new Vector2(x, y);*/

    }

    /**
     * Called when the component is enabled
     */
    void OnEnable()
    {
        body             = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    /**
     * Called on collision
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Void")
        {
            playerController.enabled = true;
            body.gravityScale        = 1;
            body.velocity            = new Vector2(0.0f, 0.0f);
        
            // Settings back the layer
            this.gameObject.layer = 8;
            Destroy(this);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, 1000.0f);
            
            if(hit)
            {
                Vector2 normal     = hit.normal;
                Vector2 reflection = Vector2.Reflect(velocity.normalized, normal);
                velocity           = reflection * initialSpeed;
            }

            body.velocity = velocity;
            AkSoundEngine.PostEvent("Sound_Bounce", gameObject);
        }
    }

    /**
     * Called when the component is enabled
     */
    public void CastSoundWave()
    {
        // Computing new velocity
        velocity = initialDirection * initialSpeed;
        
        // Body settings
        body.gravityScale = 0;
        body.velocity = velocity;
        
        // Setting the player layer
        this.gameObject.layer = 10;
        
        playerController.enabled = false;
    }
}
