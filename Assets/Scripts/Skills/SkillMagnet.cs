using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMagnet : MonoBehaviour {

    #region Attributes
    [SerializeField]
    private float m_magnetRange = 15;
    [SerializeField]
    public MagnetRay m_magnetRay;
    #endregion

    #region Methods

    public void CastMagnetWave(Vector2 initialDirection)
    {
        MagnetRay ray = Instantiate(m_magnetRay);
        ray.transform.position = transform.position;
        ray.SetDirection(initialDirection);
        ray.SetDistanceMax(m_magnetRange);
        ray.SetInitPoint(transform.position);
        Destroy(this);
    }

    #endregion
}
