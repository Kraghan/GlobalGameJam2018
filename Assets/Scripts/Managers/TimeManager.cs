using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores frame time
 * @class TimeManager
 */
public class TimeManager : MonoBehaviour
{
    public static float DeltaTime
    {
        get
        {
            return Time.deltaTime;
        }
    }

    public static float UnscaledDeltaTime
    {
        get
        {
            return Time.unscaledDeltaTime;
        }
    }

    public static float TimeScale
    {
        get
        {
            return Time.timeScale;
        }

        set
        {
            Time.timeScale      = value;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
}
