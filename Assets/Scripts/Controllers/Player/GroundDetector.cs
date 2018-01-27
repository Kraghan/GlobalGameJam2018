using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

    BoxCollider2D m_collider;
    bool m_isGrounded;
	// Use this for initialization
	void Start () {
        m_collider = gameObject.AddComponent<BoxCollider2D>();
        m_collider.isTrigger = true;

        m_isGrounded = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject != transform.parent.gameObject)
            m_isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != transform.parent.gameObject)
            m_isGrounded = false;
    }

    public bool IsGrounded()
    {
        return m_isGrounded;
    }
}
