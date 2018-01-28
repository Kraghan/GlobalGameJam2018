using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Switch : MonoBehaviour {

    #region Attributes
    [SerializeField]
    private Action m_action;

    private bool m_activated = false;
    #endregion

    #region Monobehaviour

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MagnetRay") && !m_activated)
        {
            // Set switch to triggered
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(0, 1, 0);
            // TODO !

            m_action.Trigger();
            m_activated = true;
            AkSoundEngine.PostEvent("Lever_Activate", gameObject);
        }
    }
    #endregion
}
