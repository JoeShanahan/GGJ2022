using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] GameObject quitButton;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            quitButton.SetActive(false);
    }

    public void BtnPressQuit()
    {
        Application.Quit();
    }

    public void BtnPressPlay()
    {
        TransitionManager.LoadGameScene();
    }
}
