using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class StartMenuHandler : MonoBehaviour
{
    public LoadingManager loadingManager;

    void Start()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)
            && !Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            string[] permissions = { Permission.ExternalStorageWrite, Permission.Camera };
            Permission.RequestUserPermissions(permissions);
        }
        else if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        else if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

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
