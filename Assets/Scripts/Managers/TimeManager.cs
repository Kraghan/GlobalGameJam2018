using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores frame time
 * @class TimeManager
 */
public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        AkSoundEngine.SetRTPCValue("Bullet_Time", 1.0f, TimeManager.instance.gameObject);
        instance = null;
    }

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

            // RTPC
            AkSoundEngine.SetRTPCValue("Bullet_Time", Time.timeScale, TimeManager.instance.gameObject);
        }
    }
}
