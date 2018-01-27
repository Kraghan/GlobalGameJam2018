﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Draws a picker to select the 
 * angle to cast the wave toward to
 * @class AnglePickerController
 */
public class AnglePickerController : MonoBehaviour
{
    public Vector2    axis;
    public GameObject arrow;
    public GameObject circle;
    
    /**
     * Called at start
     */
	void Start ()
    {
        // None
    }
	
    /**
     * Called each update
     */
	void Update ()
    {
        if (axis != Vector2.zero)
        {
            arrow.transform.eulerAngles = new Vector3(
                arrow.transform.eulerAngles.x,
                arrow.transform.eulerAngles.y,
                (Mathf.Atan2(axis.y, axis.x) - Mathf.PI / 2.0f) * Mathf.Rad2Deg);

            LightRaycaster(transform.position, axis, 2);
        }
    }

    /**
     * Debug only
     */
    private void LightRaycaster(Vector2 start, Vector2 dir, int maxRecursion)
    {
        if(maxRecursion == 0)
        {
            return;
        }
        else
        {
            maxRecursion -= 1;
        }

        RaycastHit2D hit = Physics2D.Raycast(start, dir, 1000.0f);
        Debug.DrawRay(start, dir * 10000.0f, Color.red);

        if (hit)
        {
            Debug.DrawRay(start, hit.point - start, Color.blue);
        
            Vector2 normal     = hit.normal;
            Vector2 reflection = Vector2.Reflect(dir, normal);

            LightRaycaster(hit.point, reflection, maxRecursion);
        }
    }

    /**
     * Called when the picker is enabled
     */
    public void OnEnable()
    {
        arrow.transform.rotation = Quaternion.identity;
    }

    /**
     * Returns the current direction of the arrow
     */
    public Vector3 GetAngle()
    {
        return new Vector2(
            Mathf.Cos(arrow.transform.rotation.eulerAngles.z),
            Mathf.Sin(arrow.transform.rotation.eulerAngles.z));
    }
}
