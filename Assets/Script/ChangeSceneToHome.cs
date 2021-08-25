using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ChangeSceneToHome : MonoBehaviour
{
    public GameObject LoadingPannel;
    public Slider slider;
    void Start()
    {
        now(1);
    }

    public void now(int sceneIndex)
    {
        StartCoroutine(LoadAsyncronysly(sceneIndex));
    }

    IEnumerator LoadAsyncronysly(int sceneIndex)
    {
        LoadingPannel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }

    }
}
