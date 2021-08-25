using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class IntroScipButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public GameObject LoadingPannel;
    public Slider slider;
    public void OnPointerDown(PointerEventData eventData)
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

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}

