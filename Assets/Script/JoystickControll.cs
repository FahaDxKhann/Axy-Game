using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJoystick;
using UnityEngine.UI;

public class JoystickControll : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public Joystick JoystickScript;
    public SfxManager SfxManagerScript;
    public BossTrigger bossTriggerScript;
    private SimpleCameraShakeInCinemachine simpleCameraShakeInCinemachine;
    public Player player;
    public GameObject RunEffect;
    public float xMovement;
    Vector3 localscale;
    Rigidbody2D rb;

    public bool RunSoundPlaying;
    public float AttackMovespeed = 0.05f;
    public bool AttackAnimationRunning = false;
    public bool SlowMOPLaying ;
    public int dashSpeed;
    bool dashing,startDash,instansRunEffect;
    public GameObject grassEffect;

    float waitTime = 5;
    float currentTime;
    public Text Timetext;

    
    void Start()
    {
        instansRunEffect = true;
        dashing = false;
        startDash = true;
        localscale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        RunSoundPlaying = false;
        SlowMOPLaying = false;
        simpleCameraShakeInCinemachine = GameObject.FindObjectOfType<SimpleCameraShakeInCinemachine>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime -= 1*Time.deltaTime;
        Timetext.text = currentTime.ToString("0.0");
        if (currentTime <= 0){currentTime = 0;}

        if (dashing == true)
        {
            if (player.OnLand == true)
            {
                GameObject grass = Instantiate(grassEffect,transform.position,Quaternion.identity);
                Destroy(grass,2f);
                //simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration; 
            }
                   
        }

        float xMovement = JoystickScript.Horizontal();
        if(xMovement>0 && player.currentHealth > 0)
        {
            Move();
        }
        if(xMovement<0 && player.currentHealth > 0)
        {
            Move();
        }
        if(xMovement == 0 && player.currentHealth > 0)
            {
                animator.SetBool("Running",false);
                SfxManagerScript.Stop("RunSound");
                RunSoundPlaying = false;
                StopCoroutine("PlayRunSoundAgainAfter_Second");
            }
 }

    public void Move()
    {
        if(bossTriggerScript.triggered == false)
        {    
    
        float xMovement = JoystickScript.Horizontal();

 //JOKHON ATTACK ANIMATION CHOLBE TOKHONKAR MOVEMENT.....................................................\/
    if(SlowMOPLaying == false){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack") && player.currentHealth > 0 )
            {
                AttackAnimationRunning = true;
                transform.position += new Vector3(xMovement,0f,0f)*AttackMovespeed*Time.deltaTime;             
                StartCoroutine("Attack_Animation_CholaKalin_Intervel");

            if(xMovement < 0 && player.currentHealth > 0)
                {
                    localscale.x = -0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                    }         
                }
            else if(xMovement > 0 && player.currentHealth > 0)
                {
                    localscale.x = 0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                    }
                }
            else if(xMovement == 0 && player.currentHealth > 0)
                {
                    animator.SetBool("Running",false);
                }
            transform.localScale = localscale;

            }


        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2") && player.currentHealth > 0 )
            {
                AttackAnimationRunning = true;
                transform.position += new Vector3(xMovement,0f,0f)*AttackMovespeed*Time.deltaTime;             
                StartCoroutine("Attack_Animation_CholaKalin_Intervel");

                if(xMovement < 0 && player.currentHealth > 0)
                {
                    localscale.x = -0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                    }
                }
            else if(xMovement > 0 && player.currentHealth > 0)
                {
                    localscale.x = 0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                    }
                }
            else if(xMovement == 0 && player.currentHealth > 0)
                {
                    animator.SetBool("Running",false);
                }
            transform.localScale = localscale;
            }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack3") && player.currentHealth > 0 )
            {
                AttackAnimationRunning = true;
                transform.position += new Vector3(xMovement,0f,0f)*AttackMovespeed*Time.deltaTime;             
                StartCoroutine("Attack_Animation_CholaKalin_Intervel");

                if(xMovement < 0 && player.currentHealth > 0)
                {
                    localscale.x = -0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                    }
                }
            else if(xMovement > 0 && player.currentHealth > 0)
                {
                    localscale.x = 0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                    }
                }
            else if(xMovement == 0 && player.currentHealth > 0)
                {
                    animator.SetBool("Running",false);
                }
            transform.localScale = localscale;
            }
    


//JOKHON ATTACK ANIMATION CHOLBE NA TOKHONKAR MOVEMENT.....................................................\/
    if(AttackAnimationRunning == false && player.currentHealth > 0)
        {
            transform.position += new Vector3(xMovement,0f,0f)*speed*Time.deltaTime;

            if(xMovement < 0 && player.currentHealth > 0 )
                {
                    localscale.x =  -0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                        if (player.OnLand == true && instansRunEffect == true)
                        {
                            Vector2 position = transform.position;
                            position.y += 0.60f;
                            position.x += 0.4f;
                            GameObject effect = WalkEffectPool.WalkEffectPoolInstanse.getPooledeffect();
                                effect.transform.position = position;
                                effect.transform.rotation = Quaternion.identity;
                                effect.SetActive(true);
                            instansRunEffect = false;
                            StartCoroutine("WaitForInstanceAgain");
                        }
                    }
                    
                }
            else if(xMovement > 0 && player.currentHealth > 0)
                {
                    localscale.x = 0.13f;
                    if(player.Grounded == true && dashing == false)
                    {
                        animator.SetBool("Running",true);
                        if (player.OnLand == true && instansRunEffect == true)
                        {
                            Vector2 position = transform.position;
                            position.y += 0.50f;
                            position.x += 0.4f;
                            GameObject effect = WalkEffectPool.WalkEffectPoolInstanse.getPooledeffect();
                                effect.transform.position = position;
                                effect.transform.rotation = Quaternion.identity;
                                effect.SetActive(true);
                            instansRunEffect = false;
                            StartCoroutine("WaitForInstanceAgain");
                        }
                    }
                }
            else if(xMovement == 0 && player.currentHealth > 0)
                {
                    animator.SetBool("Running",false);
                    SfxManagerScript.Stop("RunSound");
                    RunSoundPlaying = false;
                    StopCoroutine("PlayRunSoundAgainAfter_Second");
                }
            transform.localScale = localscale;
        }
      }
    }
    }
    
    IEnumerator WaitForInstanceAgain()
    {
        yield return new WaitForSeconds(0.5f);
        instansRunEffect = true;
    }
    IEnumerator Attack_Animation_CholaKalin_Intervel()
        {
            yield return new WaitForSeconds(0.3f);
            AttackAnimationRunning = false;
        }
    
    IEnumerator PlayRunSoundAgainAfter_Second()
        {
            yield return new WaitForSeconds(10.031f);
            RunSoundPlaying = false;
        }

    public void Dash()
    {
        if (startDash == true)
        {
            startDash = false;
            StartCoroutine("DashAgainAfter_s");
            currentTime = waitTime;
            float xMovement = JoystickScript.Horizontal();
            if (xMovement != 0)
                {
                    SfxManager.instance.PLay("DashSound");
                    dashing = true;
                    speed = 5;
                    animator.SetBool("Dash",true);
                    animator.SetBool("Running",false);
                    StartCoroutine("WaitForRestSpeed");
                }
        }
    }
    IEnumerator WaitForRestSpeed()
    {
        yield return new WaitForSeconds(0.7f);
        dashing = false;
        speed = 2;
        animator.SetBool("Dash",false);  
    }
    IEnumerator DashAgainAfter_s()
    {
        yield return new WaitForSeconds(5f);
        startDash = true; 
    }

    public void RunSound()
    {
        if (player.Grounded == true && player.OnUpland == false) { SfxManagerScript.PLay("RunSound");}
        if (player.OnUpland == true) { SfxManagerScript.PLay("WoodSound");}
    }
}

