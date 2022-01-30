using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static void LoadGameScene()
    {
        FindObjectOfType<TransitionManager>().LoadSceneByName("SampleScene");
    }

    public static void LoadTitleScene()
    {
        FindObjectOfType<TransitionManager>().LoadSceneByName("TitleScene");
    }
    
    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
