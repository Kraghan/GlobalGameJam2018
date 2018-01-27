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
    [Header("Other")]
    [SerializeField]
    private float m_groundDetectionOffset = 0.1f;

    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;
    private Animator m_animator;

    private float m_timeElapsedJump;
    private float m_distanceToTheGround;

    #endregion

    #region Monobehaviour
    void Start ()
    {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_timeElapsedJump = 0;
        m_distanceToTheGround = m_collider.bounds.extents.y - m_collider.bounds.center.y;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Movement 
        float xToAdd = Input.GetAxis("Horizontal") * m_acceleration * TimeManager.deltaTime;
        if (Mathf.Abs(m_rigidbody.velocity.x) < m_speed)
            m_rigidbody.AddForce(new Vector2(xToAdd, 0));
        if (xToAdd == 0)
            m_rigidbody.AddForce(new Vector2(-m_rigidbody.velocity.x * 2, 0));

        // Jump
        Debug.Log(IsGrounded());
        if (Input.GetButton("Jump") && IsGrounded())
            m_timeElapsedJump += TimeManager.deltaTime;
        else
            m_timeElapsedJump = 0;

        if (m_timeElapsedJump > 0 && m_timeElapsedJump <= m_timeCompleteJump)
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
        Debug.DrawRay(transform.position, -Vector3.up * (m_distanceToTheGround + m_groundDetectionOffset), Color.blue);
        return Physics.Raycast(transform.position, -Vector3.up, m_distanceToTheGround + m_groundDetectionOffset);
    }

    #endregion
}
