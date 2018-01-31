using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    public void Start()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("Music_Play");
            MusicManager.PostEvent("Wind");
        }
    }

    public void OnPlay()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("UI_Chose");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
            #endif
        }

        SceneManager.LoadSceneAsync("Main");
    }

    public void OnCreditOpen()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("UI_Chose");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
            #endif
        }

        SceneManager.LoadSceneAsync("CreditScreen");
    }

    public void OnGameExit()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("UI_Chose");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
            #endif
        }
        Application.Quit();
    }

    public void OnMainMenuBack()
    {
        if (MusicManager.WebGLBuildSupport)
        {
            MusicManager.PostEvent("UI_Chose");
        }
        else
        {
            #if !UNITY_WEBGL
                AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
            #endif
        }
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}
