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
    public Vector2           initialPoint;
    public Animator          m_animator;
    public float             distanceToDisable;
    private PlayerController playerController;

    // Physics
    public float        magnitude;
    private Rigidbody2D body;
    private Vector2     velocity;
    private float storedGravity;

    private float distanceReachedBeforeBounce;

    /**
     * Called at start
     */
    void Start()
    {
        magnitude        = 1.0f;
        body             = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        distanceReachedBeforeBounce = 0;
}

    /**
     * Called each update
     */
    void Update()
    {
        if(Vector2.Distance(initialPoint,transform.position) + distanceReachedBeforeBounce >= distanceToDisable)
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
        if (collision.gameObject.tag == "Void")
        {
            DisableSkill();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity.normalized, 1000.0f);
            
            if(hit)
            {
                Vector2 normal     = hit.normal;
                Vector2 reflection = Vector2.Reflect(velocity.normalized, normal);
                velocity           = reflection * initialSpeed;
                distanceReachedBeforeBounce += Vector2.Distance(initialPoint, transform.position);
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
        storedGravity = body.gravityScale;
        body.gravityScale = 0;
        body.velocity = velocity;
        
        // Setting the player layer
        this.gameObject.layer = 10;
        
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
