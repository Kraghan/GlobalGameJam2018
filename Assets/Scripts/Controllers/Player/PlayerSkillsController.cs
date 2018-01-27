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
    private SkillTimeFreeze       timeFreezeSkill;

    /**
     * Called at start
     */
    void Start ()
    {
        timeFreezeSkill = GetComponent<SkillTimeFreeze>();
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
            }
        }
    }
}
