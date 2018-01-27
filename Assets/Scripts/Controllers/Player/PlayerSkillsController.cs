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
    private SkillTimeFreeze  timeFreezeSkill;
    private PlayerController playerController;

    /**
     * Called at start
     */
    void Start ()
    {
        lighSkill        = GetComponent<SkillLight>();
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

            if (Input.GetButton("Form1"))
            {
                // The player is holding the light skill
                wasHoldingLight = true;
            }
            else if(wasHoldingLight)
            {
                wasHoldingLight = false;
                loadController.RemoveLoad();

                lighSkill.enabled          = true;
                lighSkill.initialDirection = new Vector2(axis.x, axis.y);

                // Finally Casts the wave
                lighSkill.CastLightWave();
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
