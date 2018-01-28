using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private Color colorActivated;
    [SerializeField]
    private float timeToChangeColor;
    [SerializeField]
    private GameObject emitter;
    private float timeElapsed;
    private Color colorInit;
    private SpriteRenderer[] renderers;
    private bool triggered;

    private void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        triggered = false;
        colorInit = renderers[0].color;
        timeElapsed = 0;
    }

    private void Update()
    {
        if(triggered)
        {
            timeElapsed += TimeManager.DeltaTime;
            foreach (SpriteRenderer renderer in renderers)
            { 
                renderer.color = Color.Lerp(colorInit, colorActivated, timeElapsed / timeToChangeColor);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !triggered)
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            controller.SetLastCheckPoint();
            triggered = true;
            emitter.SetActive(true);
        }
    }
}
