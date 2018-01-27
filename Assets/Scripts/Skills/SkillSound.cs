using UnityEngine;
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
    private float       magnitude;
    private Rigidbody2D body;
    private Vector2     velocity;

    /**
     * Called at start
     */
    void Start()
    {
        magnitude        = 10.0f;
        body             = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    /**
     * Called each update
     */
    void Update()
    {
        velocity = new Vector2(
            (velocity.normalized * Mathf.Sin(Time.time) * magnitude).x, 
            (velocity.normalized * Mathf.Sin(Time.time) * magnitude).y);
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
            body.gravityScale = 1;
            body.velocity = new Vector2(0.0f, 0.0f);
        
            // Settings back the layer
            this.gameObject.layer = 8;
            Destroy(this);

            Debug.Log("Void");
        }
        else
        {
            int contactCount = 0;
            ContactPoint2D[] contactPoints = new ContactPoint2D[1];
        
            // Gets the contact points and the normal
            contactCount = collision.GetContacts(contactPoints);

            Vector2 normal     = contactPoints[0].normal;
            Vector2 reflection = Vector2.Reflect(velocity.normalized, normal);
            velocity           = reflection * initialSpeed;

            body.velocity = velocity;
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
