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
    public  Animator         m_animator;
    private PlayerController playerController;

    // Physics
    private Rigidbody2D body;
    private Vector2     velocity;
    private float storedGravity;

    /**
     * Called at start
     */
    void Start ()
    {
        body             = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }
	
    /**
     * Called each update
     */
	void Update ()
    {
        if(Input.GetButton("CancelPower"))
        {
            DisableSkill();
        }
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
            AkSoundEngine.PostEvent("Light_Bounce", gameObject);
        }
        else
        {
            DisableSkill();
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
        if(storedGravity == 0)
            storedGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity     = velocity;

        // Setting the player layer
        this.gameObject.layer = 9;

        playerController.enabled = false;
    }

    private void DisableSkill()
    {
        playerController.enabled = true;
        body.gravityScale = storedGravity;
        body.velocity = new Vector2(0.0f, 0.0f);

        // Settings back the layer
        this.gameObject.layer = 8;
        m_animator.SetInteger("Form", 0);
        Destroy(this);
    }
}
