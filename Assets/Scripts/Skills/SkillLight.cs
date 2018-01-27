﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the ligt wave, disable the main
 * character controller
 * @class SkillLight
 */
public class SkillLight : MonoBehaviour
{
    public  float            initialSpeed;
    public  Vector2          initialDirection;
    private PlayerController playerController;

    // Physics
    private Rigidbody2D body;
    private Vector2     velocity;

    /**
     * Called at start
     */
    void Start ()
    {
        // None
    }
	
    /**
     * Called each update
     */
	void Update ()
    {
        // TODO
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
        if(collision.gameObject.tag == "Mirror")
        {
            int contactCount = 0;
            ContactPoint2D[] contactPoints = new ContactPoint2D[1];

            // Gets the contact points and the normal
            contactCount       = collision.GetContacts(contactPoints);
            Vector2 normal     = contactPoints[0].normal;
            Vector2 reflection = Vector2.Reflect(velocity.normalized, normal);

            velocity      = reflection * initialSpeed;
            body.velocity = velocity;
        }
        else
        {
            playerController.enabled = true;
            body.gravityScale        = 1;
            body.velocity            = new Vector2(0.0f, 0.0f);
        }
    }
    
    /**
     * Called when the component is enabled
     */
    public void CastLightWave()
    {
        // Computing new velocity
        velocity = initialDirection * initialSpeed;

        // Body settings
        body.gravityScale = 0;
        body.velocity     = velocity;

        playerController.enabled = false;
    }
}
