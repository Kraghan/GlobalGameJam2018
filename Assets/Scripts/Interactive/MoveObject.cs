using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : Action {
    
    [SerializeField]
    private Vector2 m_destination;
    [SerializeField]
    private float m_timeToComplete;

    private bool m_triggered;
    private float m_timeElapsed;
    private Vector2 m_initialPosition;


    private void Update()
    {
        if (m_triggered)
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
