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
    private Animator         m_animator;

    [Header("Sound form")]
    public float soundSpeed = 5;
    public float soundDistance = 10;

    [Header("Light form")]
    public float lightSpeed = 10;

    [Header("Magnet form")]
    public MagnetRay magnetRay;
    public float rayDistance = 10;

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
        m_animator = GetComponent<Animator>();
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
                if (soundSKill != null)
                    soundSKill.DisableSkill();

                lightSkill = GetComponent<SkillLight>();
                if (lightSkill == null)
                {
                    lightSkill = this.gameObject.AddComponent<SkillLight>();
                }
                else
                {
                    lightSkill.DisableSkill();
                    lightSkill = this.gameObject.AddComponent<SkillLight>();
                }
                loadController.RemoveLoad();

                wasHoldingLight = false;
                lightSkill.m_animator = m_animator;
                lightSkill.initialSpeed     = lightSpeed;
                lightSkill.initialDirection = new Vector2(axis.x, axis.y);
                lightSkill.CastLightWave();

                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("LightDash");
                }
                else
                {
                    #if !UNITY_WEBGL
                        AkSoundEngine.PostEvent("LightDash", gameObject);
                    #endif
                }

                m_animator.SetInteger("Form", 1);
            }

            // Sound
            if (Input.GetButton("Form2"))
            {
                wasHoldingSound = true;
                timeFreezeSkill.Freeze();
            }
            else if (wasHoldingSound)
            {
                soundSKill = GetComponent<SkillSound>();
                if (lightSkill != null)
                    lightSkill.DisableSkill();

                if (soundSKill == null)
                {
                    soundSKill = this.gameObject.AddComponent<SkillSound>();
                }
                else
                {
                    soundSKill.DisableSkill();
                    soundSKill = this.gameObject.AddComponent<SkillSound>();
                }
                loadController.RemoveLoad();

                wasHoldingSound = false;

                soundSKill.m_animator = m_animator;
                soundSKill.initialSpeed     = soundSpeed;
                soundSKill.initialDirection = new Vector2(axis.x, axis.y);
                soundSKill.initialPoint = transform.position;
                soundSKill.distanceToDisable = soundDistance;
                soundSKill.CastSoundWave();

                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("SoundDash");
                }
                else
                {
                    #if !UNITY_WEBGL
                        AkSoundEngine.PostEvent("SoundDash", gameObject);
                    #endif
                }

                m_animator.SetInteger("Form", 2);
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
                    magnetSKill = this.gameObject.AddComponent<SkillMagnet>();
                }
                loadController.RemoveLoad();

                wasHoldingMagnet = false;

                Vector2 initialDirection = new Vector2(axis.x, axis.y);
                magnetSKill.m_magnetRay = magnetRay;
                magnetSKill.m_magnetRay.SetDistanceMax(rayDistance);
                magnetSKill.CastMagnetWave(initialDirection);

                if (MusicManager.WebGLBuildSupport)
                {
                    MusicManager.PostEvent("Magnet_Sound");
                }
                else
                {
                    #if !UNITY_WEBGL
                        AkSoundEngine.PostEvent("Magnet_Sound", gameObject);
                    #endif
                }

            }


            // TODO check
        }

        // The player touched the ground
        // So we can reload the wave loads
        if(playerController.IsGrounded() && magnetSKill == null && lightSkill == null && soundSKill == null)
        {
            loadController.Reload();
        }
    }
}
