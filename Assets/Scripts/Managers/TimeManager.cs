using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores frame time
 * @class TimeManager
 */
public class TimeManager : MonoBehaviour
{
    public static float deltaTime;
    public static float timeScale;

    /**
     * Called at start
     */
	void Start ()
    {
        TimeManager.deltaTime = 0.0f;
        TimeManager.timeScale = 1.0f;
    }
	
    /**
     * Called each update
     */
	void Update ()
    {
        TimeManager.deltaTime = TimeManager.timeScale * Time.deltaTime;
    }
}
