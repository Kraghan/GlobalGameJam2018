using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Controls UI wave loads
 * @class WaveLoadsController
 */
public class WaveLoadsController : MonoBehaviour
{
    private int         loadCount;
    public  List<Image> loads      = new List<Image>();
    
    /**
     * Called at start
     */
	void Start ()
    {
        loadCount = loads.Count;
    }

    /**
     * Makes all loads available
     */
    public void Reload()
    {
        loadCount = loads.Count;
        for(int nLoad = 0; nLoad < loadCount; ++nLoad)
        {
            loads[nLoad].enabled = true;
        }
    }

    /**
     * Removes a load
     */
    public void RemoveLoad()
    {
        if(loadCount >= 1)
        {
            loads[loadCount - 1].enabled = false;
            loadCount -= 1;
        }
    }

    /**
     * Tells the current available loads count
     */
    public int GetAvailableLoadCount()
    {
        return loadCount;
    }

    /**
     * Tells the maximum loads count
     */
    public int GetMaxLoadCount()
    {
        return loads.Count;
    } 
}
