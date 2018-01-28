using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages the time freeze skill
 * @class SkillTimeFreeze
 */
public class SkillTimeFreeze : MonoBehaviour
{
    public float                timeScale;
    public TimerGaujeController gaujeController;

    private bool isFreezing;

	/**
     * Called at start
     */
	void Start ()
    {
        isFreezing = false;
    }
	
    /**
     * Called each update
     */
	void Update ()
    {
        if(isFreezing)
        {
            TimeManager.TimeScale = timeScale;
            gaujeController.EmptyGauje();

            // RTPC
            Debug.Log("RTPC event : " + Time.timeScale);
            AkSoundEngine.SetRTPCValue("Bullet_Time", Time.timeScale);

        }
        else
        {
            TimeManager.TimeScale = 1.0f;
            AkSoundEngine.SetRTPCValue("Bullet_Time", Time.timeScale);
        }

        isFreezing = false;
    }

    /**
     * Tells if the player can slow down the time or not
     * @return true or false
     */
    public bool CanFreeze()
    {
        return gaujeController.IsFilled();
    }

    /**
     * Apply a big slow down on the game
     * (only if the player can freeze the time)
     */
    public void Freeze()
    {
        if(CanFreeze())
        {
            isFreezing = true;
        }
    }
}
