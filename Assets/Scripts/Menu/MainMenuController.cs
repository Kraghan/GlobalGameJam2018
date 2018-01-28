using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    public void OnPlay()
    {
        AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
        SceneManager.LoadSceneAsync("Main");
    }

    public void OnCreditOpen()
    {
        AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
        SceneManager.LoadSceneAsync("CreditScreen");
    }

    public void OnGameExit()
    {
        AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
        Application.Quit();
    }

    public void OnMainMenuBack()
    {
        AkSoundEngine.PostEvent("UI_Chose", this.gameObject);
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}
