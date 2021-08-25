using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    private static UiManager instance;
    public GameObject Player;
    public Animator PLayerAnim;
    public Player playerScript;
    public SfxManager sfxManagerScript;
    public GameObject GameoverPannel;

    public GameObject LoadingPannel;
    public GameObject pauseMenu;
    public GameObject HealthBar;
    public Slider slider;
    public GameObject StartPoint;





    void Awake(){
		if(instance == null){
			instance = this;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("1");
        }
        if(playerScript.PlayerDied == true)
        {
            GameoverPannel.SetActive(true);
        }
    }

    public void RelodeButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        GameoverPannel.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void HomeButton(int sceneIndex)
    {
        pauseMenu.SetActive(false);
        sfxManagerScript.PlayOneStop("-mouse-click");
        StartCoroutine(LoadAsyncronysly(sceneIndex));
        Time.timeScale = 1;
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

    public void RestartButton()
    {
        sfxManagerScript.PlayOneStop("-mouse-click");
        Player.transform.position = StartPoint.transform.position;
        playerScript.coins = 0;
        playerScript.HealthEqualsTo(100);
        GameoverPannel.SetActive(false);
        playerScript.PlayerDied = false;
        PLayerAnim.Play("PlayerIdle");
    }
}
