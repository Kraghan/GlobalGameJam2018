using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages player skills
 * @class PlayerSkillsController
 */
public class PlayerSkillsController : MonoBehaviour
{
    public  WaveLoadsController   loadController;
    public  AnglePickerController pickerController;

    private bool wasHoldingLight;
    private bool wasHoldingSound;
    private bool wasHoldingMagnet;

    private SkillLight       lighSkill;
    private SkillSound       soundSKill;
    private SkillTimeFreeze  timeFreezeSkill;
    private PlayerController playerController;

    /**
     * Called at start
     */
    void Start ()
    {
        timeFreezeSkill  = GetComponent<SkillTimeFreeze>();
        playerController = GetComponent<PlayerController>();

        wasHoldingLight  = false;
        wasHoldingSound  = false;
        wasHoldingMagnet = false;
    }
	
	/**
     * Called each update
     */
	void Update ()
    {
        if (Input.GetAxis("TimeFreeze") >= 0.5f)
        {
            timeFreezeSkill.Freeze();
        }

        if (loadController.GetAvailableLoadCount() > 0)
        {
            Vector2 axis = Vector2.zero;
            axis.x = Input.GetAxis("Horizontal");
            axis.y = Input.GetAxis("Vertical");

            if(Input.GetButton("Form1") || 
               Input.GetButton("Form2") || 
               Input.GetButton("Form3"))
            {
                // The player is holding a skill
                pickerController.gameObject.SetActive(true);
                pickerController.axis = axis;
            }
            else
            {
                pickerController.gameObject.SetActive(false);
            }

            // Light
            if (Input.GetButton("Form1"))
            {
                wasHoldingLight = true;
            }
            else if(wasHoldingLight)
            {
                if(GetComponent<SkillLight>() == null)
                {
                    loadController.RemoveLoad();
                    lighSkill = this.gameObject.AddComponent<SkillLight>();
                }
            
                wasHoldingLight = false;
                lighSkill.initialSpeed     = 10.0f;
                lighSkill.initialDirection = new Vector2(axis.x, axis.y);
                lighSkill.CastLightWave();
            }

            // Sound
            if (Input.GetButton("Form2"))
            {
                wasHoldingSound = true;
            }
            else if (wasHoldingSound)
            {
                Debug.Log("Casting sound wave");
                if (GetComponent<SkillSound>() == null)
                {
                    loadController.RemoveLoad();
                    soundSKill = this.gameObject.AddComponent<SkillSound>();
                }

                wasHoldingSound = false;
                soundSKill.initialSpeed     = 5.0f;
                soundSKill.initialDirection = new Vector2(axis.x, axis.y);
                soundSKill.CastSoundWave();
            }
        }

        // The player touched the ground
        // So we can reload the wave loads
        if(playerController.IsGrounded())
        {
            loadController.Reload();
        }
    }
}
