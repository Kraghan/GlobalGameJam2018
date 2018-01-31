using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MoveObject
{
    [SerializeField]
    private Sprite m_openSprite;
    private bool once = true;
    protected override void Update()
    {
        base.Update();
        if (once && m_timeElapsed >= m_timeToComplete)
        {
            if (MusicManager.WebGLBuildSupport)
            {
                MusicManager.PostEvent("Door_Move_Stop");
                MusicManager.PostEvent("Door_Stop");
            }
            else
            {
                #if !UNITY_WEBGL
                    AkSoundEngine.PostEvent("Door_Move_Stop", gameObject);
                    AkSoundEngine.PostEvent("Door_Stop", gameObject);
                #endif
            }

            once = false;
        }
    }

    public override void Trigger()
    {
        base.Trigger();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = m_openSprite;

        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Door_Move_Play");
        }
        else
        {
            #if !UNITY_WEBGL
                 AkSoundEngine.PostEvent("Door_Move_Play", gameObject);
            #endif
        }
    }
}
