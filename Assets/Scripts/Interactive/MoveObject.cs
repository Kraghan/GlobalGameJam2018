using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Action {
    
    [SerializeField]
    protected Vector2 m_destination;
    [SerializeField]
    protected float m_timeToComplete;

    protected bool m_triggered;
    protected float m_timeElapsed;
    private Vector2 m_initialPosition;


    protected virtual void Update()
    {
        if (m_triggered && m_timeElapsed <= m_timeToComplete)
        {
            m_timeElapsed += TimeManager.DeltaTime;
            transform.position = Vector2.Lerp(m_initialPosition, m_destination, m_timeElapsed/m_timeToComplete);
        }
    }

    public override void Trigger()
    {
        m_initialPosition = transform.position;
        m_triggered = true;

    }
}
