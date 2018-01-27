using UnityEngine;
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
            contactCount   = collision.GetContacts(contactPoints);
            Vector2 normal = contactPoints[0].normal;

            Debug.Log("Normal : " + normal);

            // Computing incidence angle
            float angle = Vector2.Angle(normal, velocity);
            Debug.Log("Velocity : " + velocity.normalized);
            Debug.Log("Angle : " + angle);
        }
    }
    
    /**
     * Called when the component is enabled
     */
    public void CastLightWave()
    {
        // Body settings
        body.gravityScale = 0;
        body.AddForce(initialDirection * initialSpeed);

        velocity = initialDirection * initialSpeed;

        Debug.Log(initialDirection * initialSpeed);
        playerController.enabled = false;

        Debug.Log("Casted");
    }
}
