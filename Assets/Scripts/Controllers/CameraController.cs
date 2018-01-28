using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private float m_deadZone = 15;
    [SerializeField]
    private float m_scrollSpeed = 5;
    private GameObject m_player;
    private Camera m_camera;

	// Use this for initialization
	void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float deadZoneSize = Screen.width / 100.0f * m_deadZone;

        Vector2 posOnScreen = m_camera.WorldToScreenPoint(m_player.transform.position);
        if (posOnScreen.x <= deadZoneSize)
        {
            transform.position = transform.position - new Vector3(m_scrollSpeed * TimeManager.DeltaTime,0,0);
        }
        else if (posOnScreen.x >= Screen.width - deadZoneSize)
        {
            transform.position = transform.position + new Vector3(m_scrollSpeed * TimeManager.DeltaTime, 0, 0);
        }
    }
}
