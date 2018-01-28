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

    private SkillLight       lightSkill;
    private SkillSound       soundSKill;
    private SkillMagnet      magnetSKill;
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

        if (loadController.GetAvailableLoadCount() > 0)
        {
            Vector2 axis = Vector2.zero;
            axis.x = Input.GetAxis("Horizontal");
            axis.y = Input.GetAxis("Vertical");

            if (axis == Vector2.zero)
                axis = new Vector2(0, 1);

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
                timeFreezeSkill.Freeze();
            }
            else if(wasHoldingLight)
            {
                lightSkill = GetComponent<SkillLight>();
                if (GetComponent<SkillLight>() == null)
                {
                    loadController.RemoveLoad();
                    lightSkill = this.gameObject.AddComponent<SkillLight>();
                }
            
                wasHoldingLight = false;
                lightSkill.initialSpeed     = 10.0f;
                lightSkill.initialDirection = new Vector2(axis.x, axis.y);
                lightSkill.CastLightWave();
                AkSoundEngine.PostEvent("LightDash", gameObject);
            }

            // Sound
            if (Input.GetButton("Form2"))
            {
                wasHoldingSound = true;
                timeFreezeSkill.Freeze();
            }
            else if (wasHoldingSound)
            {
                Debug.Log("Casting sound wave");
                soundSKill = GetComponent<SkillSound>();
                if (soundSKill == null)
                {
                    loadController.RemoveLoad();
                    soundSKill = this.gameObject.AddComponent<SkillSound>();
                }

                wasHoldingSound = false;
                soundSKill.initialSpeed     = 5.0f;
                soundSKill.initialDirection = new Vector2(axis.x, axis.y);
                soundSKill.CastSoundWave();
                AkSoundEngine.PostEvent("SoundDash", gameObject);
            }

            // Magnet
            if (Input.GetButton("Form3"))
            {
                wasHoldingMagnet = true;
                timeFreezeSkill.Freeze();
            }
            else if (wasHoldingMagnet)
            {
                magnetSKill = GetComponent<SkillMagnet>();
                if (magnetSKill == null)
                {
                    loadController.RemoveLoad();
                    magnetSKill = this.gameObject.AddComponent<SkillMagnet>();
                }

                wasHoldingMagnet = false;

                Vector2 initialDirection = new Vector2(axis.x, axis.y);
                magnetSKill.CastMagnetWave(initialDirection);
                AkSoundEngine.PostEvent("Magnet_Sound", gameObject);
            }


            // TODO check
        }

        // The player touched the ground
        // So we can reload the wave loads
        if(playerController.IsGrounded())
        {
            loadController.Reload();
        }
    }
}
