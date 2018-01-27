using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/**
 * Controls the timer gauje
 * @class TimerGaujeController
 */
public class TimerGaujeController : MonoBehaviour
{
    public float gaujeDuration;
    public float gaujeRefillDelay;
    public float gaujeRefillDuration;

    public Image gaujeImage;
    public Image gaujeFillImage;

    private enum EGaujeState
    {
        Emptying,
        Filled,
        Filling,
        Cooldown
    }

    private bool        isHolding;
    private EGaujeState gaujeState;
    private IEnumerator gaujeFillCoroutine;

    /**
     * Called at start
     */
	void Start ()
    {
        gaujeState = EGaujeState.Filled;

        // Starts the refill coroutines
        StartCoroutine("FillGaujeCoroutine");
    }

    /**
     * Called each update
     */
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            EmptyGauje();
        }

        /**
         * Local state machine
         */
        switch (gaujeState)
        {
            case EGaujeState.Emptying: GaujeEmptyingState();  break;
            case EGaujeState.Filled:   GaujeFilledState();    break;
            case EGaujeState.Filling:  GaujeFillingState();   break;
            case EGaujeState.Cooldown: GaujeCooldownState();  break;
            default: break;
        }

        isHolding = false;
    }

    /**
     * If the gauje is filled, empties it
     * When the gauje is emptied, refill it
     */
    public void EmptyGauje()
    {
        isHolding = true;
    }

    /**
     * Tells if the gauje is filled enough to slow time
     * @return true or false
     */
    public bool IsFilled()
    {
        return gaujeState == EGaujeState.Filled ||
               gaujeState == EGaujeState.Emptying;
    }

    /**
     * Called when the gauje is emptying
     */
    private void GaujeEmptyingState()
    {
        if (isHolding)
        {
            gaujeFillImage.fillAmount -= Time.deltaTime / gaujeDuration;

            if(gaujeFillImage.fillAmount == 0.0f)
            {
                gaujeState = EGaujeState.Filling;
                StartCoroutine("FullFillGaujeCoroutine");
            }
        }
        else
        {
            gaujeState = EGaujeState.Cooldown;
            StartCoroutine("WaitForRefillCoroutine");
        }
    }

    /**
     * Called when the gauje is filled
     */
    private void GaujeFilledState()
    {
        if(isHolding)
        {
            gaujeState = EGaujeState.Emptying;
        }
    }

    /**
     * Hook
     * Called when the gauje is filling
     */
    private void GaujeFillingState()
    {
        // None
    }

    /**
     * Called when the gauje is in cooldown
     */
    private void GaujeCooldownState()
    {
        // None
    }

    /**
     * Waits the cooldown before refilling
     */
    private IEnumerator WaitForRefillCoroutine()
    {
        yield return new WaitForSeconds(gaujeRefillDelay);
        gaujeState = EGaujeState.Filled;
    }

    /**
     * Fills the gauje continually 
     */
    private IEnumerator FillGaujeCoroutine()
    {
        while(true)
        {
            if(gaujeState == EGaujeState.Filled && gaujeFillImage.fillAmount < 1.0f) 
            {
                gaujeFillImage.fillAmount += Time.deltaTime / gaujeRefillDuration;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    /**
     * Makes the gauje full
     */
    private IEnumerator FullFillGaujeCoroutine()
    {
        while (gaujeFillImage.fillAmount != 1.0f)
        {
            gaujeFillImage.fillAmount += Time.deltaTime / gaujeRefillDuration;
            yield return new WaitForEndOfFrame();
        }

        gaujeState = EGaujeState.Filled;
    }
}
