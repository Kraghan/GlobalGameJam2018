using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {

    #region Attributes
    [Header("Movement")]
    [SerializeField]
    private float m_speed = 1.5f;
    [SerializeField]
    private float m_acceleration = 1.5f;
    [Header("Jump")]
    [SerializeField]
    private float m_timeCompleteJump = 1.5f;
    [SerializeField]
    private float m_jumpForce = 10;

    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;

    private float m_timeElapsedJump;
    private bool m_isGrounded;
    private GroundDetector m_groundDetector;
    private bool m_canJump;
    #endregion

    #region Monobehaviour
    void Start ()
    {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_timeElapsedJump = 0;

        // Add ground detector
        Vector3 playerPos = transform.position;
        transform.position = new Vector3(0, 0, 0);
        GameObject groundDetector = new GameObject("Ground Detector");
        groundDetector.transform.parent = transform;
        groundDetector.transform.position = transform.position + m_collider.bounds.center - new Vector3(0, m_collider.bounds.size.y * transform.localScale.y, 0);
        m_groundDetector = groundDetector.AddComponent<GroundDetector>();
        transform.position = playerPos;
        m_canJump = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Movement 
        float xToAdd = Input.GetAxis("Horizontal") * m_acceleration * TimeManager.DeltaTime;
        if (Mathf.Abs(m_rigidbody.velocity.x) < m_speed)
            m_rigidbody.AddForce(new Vector2(xToAdd, 0));
        if (xToAdd == 0)
            m_rigidbody.AddForce(new Vector2(-m_rigidbody.velocity.x * 2, 0));

        // Jump

        if (Input.GetButton("Jump"))
        {
            m_timeElapsedJump += TimeManager.DeltaTime;
            m_canJump = false;
        }
        else
        {
            if (IsGrounded())
            {
                m_timeElapsedJump = 0;
            }
            else
                m_timeElapsedJump = m_timeCompleteJump;
            
        }
            

        if (m_timeElapsedJump > 0 && m_timeElapsedJump < m_timeCompleteJump)
        {
            float yToAdd = Mathf.Lerp(m_jumpForce,0,m_timeElapsedJump/m_timeCompleteJump);
            m_rigidbody.AddForce(new Vector2(0, yToAdd));
        }
        // Animator
        m_animator.SetBool("IsGrounded", IsGrounded());
        m_animator.SetFloat("XVelocity", Mathf.Abs(m_rigidbody.velocity.x));
        if ((m_rigidbody.velocity.x < 0 && transform.localScale.x > 0) ||
            (m_rigidbody.velocity.x > 0 && transform.localScale.x < 0))
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        m_animator.SetFloat("YVelocity", m_rigidbody.velocity.y);
    }
    #endregion

    #region Methods
    public bool IsGrounded()
    {
        return m_groundDetector.IsGrounded();
    }

    #endregion
}
