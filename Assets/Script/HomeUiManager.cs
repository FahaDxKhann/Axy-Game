using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUiManager : MonoBehaviour
{
    public static HomeUiManager instance;
    public SfxManager sfxmanagerScript;
    public GameObject LoadingPannel;
    public Slider slider;

    public Animator PlayerAnimator;
    public Player PlayerScript;
    public GameObject player;
    public Shop shopScript;
    public bool continueButtonPressed;

    public Animator StartNowBAnim;
    public Animator ContinueBAnim;
    public Animator SettingsBAnim;
    public Animator ExitBAnim;



    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }   
    }
    private void Start() 
    {
        continueButtonPressed = false;
    }
    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }    
    }
    public void StartNewButton( int sceneIndex)
    {
        StartNowBAnim.SetTrigger("Click");
        sfxmanagerScript.PLay("ClickSound1");
        StartCoroutine(LoadAsyncronysly(sceneIndex));
    }
    public void ContinueButton( int sceneIndex)
    {
        continueButtonPressed = true;
        sfxmanagerScript.PLay("ClickSound1");
        ContinueBAnim.SetTrigger("Click");
        StartCoroutine(LoadAsyncronyslyContinue(sceneIndex));
    }

    IEnumerator LoadAsyncronysly(int sceneIndex)
    {
        LoadingPannel.SetActive(true);
        AsyncOperation operation =  SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            
            yield return null;
        }

    }

    IEnumerator LoadAsyncronyslyContinue(int sceneIndex)
    {
       

        LoadingPannel.SetActive(true);
        AsyncOperation operation =  SceneManager.LoadSceneAsync(sceneIndex);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            
            yield return null;
        }

    }

    public void StartNowButton()
    {
        
    }
    public void ContinueButton()
    {
        
    }
    public void SettingsButton()
    {
        SettingsBAnim.SetTrigger("Click");
        sfxmanagerScript.PLay("ClickSound1");
        StartCoroutine("WaitForOperation1");
    }
    IEnumerator WaitForOperation1()
    {
        yield return new WaitForSeconds(0.3f);

    }

    public void Exit()
    {
        ExitBAnim.SetTrigger("Click");
        sfxmanagerScript.PLay("ClickSound1");
        StartCoroutine("WaitForOperation2");
    }
    IEnumerator WaitForOperation2()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }
}
