using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Wall WallScript;
    public GameObject MainCamera;
    public GameObject WallCamera;
    public GameObject Joystic;
    public GameObject AttackButton;
    public GameObject JumpButton;
    Animator animator;
    [HideInInspector]
    public bool AnimationPlayedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationPlayedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(AnimationPlayedOnce == false)
            {
            Joystic.SetActive(false);
            JumpButton.SetActive(false);
            AttackButton.SetActive(false);
            animator.SetBool("On",true);
            StartCoroutine("WaitFor30sToActiveWallAnim");
            AnimationPlayedOnce = true;
            SfxManager.instance.PLay("LeverSound");
            }
        }
    }

    IEnumerator WaitFor30sToActiveWallAnim()
    {
        yield return new WaitForSeconds(1.2f);
        WallScript.nowStart = true;
        MainCamera.SetActive(false);
        WallCamera.SetActive(true);
        StartCoroutine("EnableMainCameraBack");

    }

    IEnumerator EnableMainCameraBack()
    {
        yield return new WaitForSeconds(5f);
        Joystic.SetActive(true);
        JumpButton.SetActive(true);
        AttackButton.SetActive(true);
        MainCamera.SetActive(true);
        WallCamera.SetActive(false);
    }
        
}
