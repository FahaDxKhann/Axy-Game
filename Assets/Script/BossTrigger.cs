using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public SfxManager sfxManagerScript;
    public GameObject BossCamera;
    public GameObject Boss;
    public GameObject Joystic;
    public GameObject AttackButton;
    public GameObject JumpButton;
    public Animator BossAniamtor;
    public Animator PlayerAnim;
    public Transform TargetPosition;
    public float speed;
    bool nowMove,triggerOnce;
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggerOnce = false;
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowMove == true)
        {
            Boss.transform.position = Vector3.MoveTowards(Boss.transform.position,TargetPosition.transform.position,speed*Time.deltaTime);
        }
        if(Boss.transform.position == TargetPosition.transform.position)
            {
                BossAniamtor.SetBool("Walk",false);
                BossAniamtor.SetBool("Talk1",true);
                nowMove = false;
                
            }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(triggerOnce == false)
        {
            if(other.gameObject.tag == "Player")
            {
                triggered = true;
                triggerOnce = true;
                Joystic.SetActive(false);
                JumpButton.SetActive(false);
                AttackButton.SetActive(false);
                BossCamera.SetActive(true);
                BossAniamtor.SetBool("Walk",true);
                PlayerAnim.SetBool("Running",false);
                sfxManagerScript.Stop("RunSound");
                nowMove = true;
            
            
            }
        }
    }

    
}
