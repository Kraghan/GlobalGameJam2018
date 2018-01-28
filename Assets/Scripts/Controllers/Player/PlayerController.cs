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
    private float m_distToGround;
    private Vector2 m_lastCheckPoint;
    #endregion

    #region Monobehaviour
    void Start ()
    {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_timeElapsedJump = 0;
        Vector3 oldPos = transform.position;
        transform.position = Vector3.zero;
        m_distToGround = m_collider.bounds.extents.y - m_collider.bounds.center.y;
        transform.position = oldPos;

        SetLastCheckPoint();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("ReturnCheckpoint"))
        {
            transform.position = m_lastCheckPoint;
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        // Movement 
        float xToAdd = Input.GetAxis("Horizontal") * m_acceleration * TimeManager.DeltaTime;
        if (Mathf.Abs(m_rigidbody.velocity.x) < m_speed)
            m_rigidbody.AddForce(new Vector2(xToAdd, 0));
        if (xToAdd == 0)
            m_rigidbody.velocity = new Vector2(0,m_rigidbody.velocity.y);

        // Jump

        if (Input.GetButton("Jump"))
        {
            m_timeElapsedJump += TimeManager.DeltaTime;
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
        m_animator.speed = Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f && IsGrounded() ? Mathf.Abs(Input.GetAxis("Horizontal")) : 1;
    }
    #endregion

    #region Methods
    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -new Vector3(0,m_distToGround + 0.1f,0), Color.green);
        return Physics2D.Raycast(transform.position, -Vector3.up, m_distToGround + 0.1f);
    }

    public void PlaySound(string soundName)
    {
        AkSoundEngine.PostEvent(soundName, gameObject);
    }

    public void SetLastCheckPoint()
    {
        m_lastCheckPoint = transform.position;
    }

    #endregion
}
