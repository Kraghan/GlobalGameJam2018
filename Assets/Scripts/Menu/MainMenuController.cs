using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void OnCreditOpen()
    {
        SceneManager.LoadSceneAsync("CreditScreen");
    }

    public void OnGameExit()
    {
        Application.Quit();
    }

    public void OnMainMenuBack()
    {
        SceneManager.LoadSceneAsync("TitleScreen");
    }
}
