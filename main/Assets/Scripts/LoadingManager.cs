using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Image progressBar;
    public GameObject loadingWheel;
    public float wheelSpeed;

    private string nextScene;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        nextScene = sceneName;
        gameObject.SetActive(true);
        StartCoroutine(LoadScene());
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == nextScene)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;

        StartCoroutine(spinWheelRoutine());
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.6f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer / 30);
                if (progressBar.fillAmount >= 0.99f)
                {
                    op.allowSceneActivation = true;
                    StartCoroutine(loadingEnd(op));
                    yield break;
                }
            }
        }
    }

    IEnumerator loadingEnd(AsyncOperation op)
    {
        yield return new WaitForSecondsRealtime(2f);

        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, Time.deltaTime / 20);
        StopCoroutine(spinWheelRoutine());
        Destroy(gameObject);
    }

    IEnumerator spinWheelRoutine()
    {
        while (true)
        {
            loadingWheel.transform.Rotate(0, 0, -wheelSpeed);
            yield return null;
        }
    }
}
