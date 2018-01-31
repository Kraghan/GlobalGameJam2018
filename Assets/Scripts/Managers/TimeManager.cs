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

    private void Start()
    {
        StartCoroutine("Droplets");
    }

    private void OnDestroy()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.SetMainVolume(1.0f);
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.SetRTPCValue("Bullet_Time", 1.0f, TimeManager.instance.gameObject);
            #endif
        }

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
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.SetMainVolume(Time.timeScale);
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.SetRTPCValue("Bullet_Time", Time.timeScale, TimeManager.instance.gameObject);
                #endif
            }
        }
    }

    private IEnumerator Droplets()
    {
        while(true)
        {
            float newDelay = Random.Range(4.0f, 10.0f);
            yield return new WaitForSeconds(newDelay);

            if(MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Droplets");
            }
        }
    }
}
