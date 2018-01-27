using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRay : MonoBehaviour {

    #region Attributes
    [SerializeField]
    private float rayTrailLength = 5;
    private Vector2 m_direction;
    private float m_speed;
    private Vector2 m_initPoint;
    private float m_distanceMax;
    #endregion
	
	// Update is called once per frame
	void Update ()
    {
        //transform.position = new Vector2(Mathf.Cos()*m_speed,Mathf.Sin()*m_speed);
	}

    #region Methods
    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
    }

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    public void SetInitPoint(Vector2 initPoint)
    {
        m_initPoint = initPoint;
    }

    public void SetDistanceMax(float distance)
    {
        m_distanceMax = distance;
    }
    #endregion
}
