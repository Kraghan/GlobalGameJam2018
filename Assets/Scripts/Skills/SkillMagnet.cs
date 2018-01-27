using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMagnet : MonoBehaviour {

    #region Attributes
    [SerializeField]
    private float m_magnetRange = 15;
    [SerializeField]
    private MagnetRay m_magnetRay;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion

    #region Methods

    public void CastMagnetWave(Vector2 initialDirection)
    {
        MagnetRay ray = Instantiate(m_magnetRay);
        ray.transform.position = transform.position;
        ray.SetDirection(initialDirection);
        ray.SetDistanceMax(m_magnetRange);
        ray.SetInitPoint(transform.position);
    }

    #endregion
}
