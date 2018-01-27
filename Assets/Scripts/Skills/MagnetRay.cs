using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetRay : MonoBehaviour
{

    #region Attributes
    [SerializeField]
    private float m_speed = 1;
    private Vector2 m_direction;
    private Vector2 m_initPoint;
    private float m_distanceMax;
    #endregion

    // Update is called once per frame
    void Update()
    {
        float angle = (Vector2.Angle(m_direction, Vector2.right) * Mathf.PI) / 180;
        if (m_direction.y < 0)
            angle *= -1; 
        transform.position = transform.position + new Vector3(Mathf.Cos(angle) * m_speed, Mathf.Sin(angle) * m_speed,0);
        if (Vector2.Distance(m_initPoint, transform.position) >= m_distanceMax)
            Destroy(gameObject);

    }

    #region Methods
    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
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
