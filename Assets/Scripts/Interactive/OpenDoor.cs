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
            AkSoundEngine.PostEvent("Door_Move_Stop", gameObject);
            AkSoundEngine.PostEvent("Door_Stop", gameObject);
            once = false;
            Debug.Log("Toto");
        }
    }

    public override void Trigger()
    {
        base.Trigger();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = m_openSprite;
        AkSoundEngine.PostEvent("Door_Move_Play", gameObject);
    }
}
