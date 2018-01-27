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
        lighSkill        = GetComponent<SkillLight>();
        soundSKill       = GetComponent<SkillSound>();
        timeFreezeSkill  = GetComponent<SkillTimeFreeze>();
        playerController = GetComponent<PlayerController>();

        wasHoldingLight  = false;
        wasHoldingSound  = false;
        wasHoldingMagnet = false;

        lighSkill.enabled = false;
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
                wasHoldingLight = false;
                loadController.RemoveLoad();

                lighSkill.enabled          = true;
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
                wasHoldingSound = false;
                loadController.RemoveLoad();

                soundSKill.enabled = true;
                soundSKill.initialDirection = new Vector2(axis.x, axis.y);

                soundSKill.CastSoundWave();
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
