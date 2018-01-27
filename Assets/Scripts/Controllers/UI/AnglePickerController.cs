using UnityEngine;
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
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");

        if (axis != Vector2.zero)
        {
            arrow.transform.eulerAngles = new Vector3(
                arrow.transform.eulerAngles.x,
                arrow.transform.eulerAngles.y,
                (Mathf.Atan2(axis.y, axis.x) - Mathf.PI / 2.0f) * Mathf.Rad2Deg);
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
        return arrow.transform.rotation.eulerAngles;
    }
}
