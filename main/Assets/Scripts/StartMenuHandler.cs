using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuHandler : MonoBehaviour
{
    public LoadingManager loadingManager;

    public void loadScene(string sceneName)
    {
        loadingManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // quit when running a build (not in editor)
        Application.Quit();
    }
}
