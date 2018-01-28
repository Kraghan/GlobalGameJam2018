using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCurve : MonoBehaviour {
    public float amplitudeY;
    public float frequence;
    public float dist;
    public float sens = 0.1f;
    public float angle;
    private float xPos = 0;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.position.x < -dist || transform.position.x > dist)
            sens *= -1;

        xPos += sens;

        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        float y = sin * (xPos) + cos * Mathf.Sin(xPos * frequence) * amplitudeY;
        float x = cos * (xPos) - sin * Mathf.Sin(xPos * frequence) * amplitudeY;

        transform.position = new Vector3(x, y);
	}
}
