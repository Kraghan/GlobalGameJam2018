using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float m_speed = 1.5f;
    [SerializeField]
    private float m_acceleration = 1.5f;

    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;

	// Use this for initialization
	void Start ()
    {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float xToAdd = Input.GetAxis("Horizontal") * m_acceleration * TimeManager.deltaTime;
        if(Mathf.Abs(m_rigidbody.velocity.x) < m_speed)
            m_rigidbody.AddForce(new Vector2(xToAdd, 0));
        if (xToAdd == 0)
            m_rigidbody.AddForce(new Vector2(-m_rigidbody.velocity.x * 2, 0));
	}
}
