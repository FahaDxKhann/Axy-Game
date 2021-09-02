using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject player;
    public Sattack SattackScript;
    public Healthbar healthbarscript;
    public SfxManager SfxManagerScript;
    public Lever LeverScript;
    public Boss bossScript;
    public Shop shopScript;
    private JoystickControll joystickControll;
    private SimpleCameraShakeInCinemachine simpleCameraShakeInCinemachine;
    public GameObject HitEffect;
    public GameObject HealEffect;
    public GameObject Rock;
    private int RockDamage = 100;
    private int SpikeDamage = 100;
    public static Player instance;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    public Animator LightAnim;
    [HideInInspector]
    public bool Grounded = true;
    public float SpringJumpSpeed;
    private bool NowHit;
    public LayerMask EnemyLayers;
    public LayerMask TowerLayer;

    public Vector2 RockIntantiatePosition;
    public Vector2 RockIntantiatePosition2;
    bool InstantiateRock,InstantiateRock2;

    private int comboStep;
    private float comboTime;
    private float comboTime2;
    private float comboTime3;
    public float comboTimeDiffrence;
    private float TimeDiffrence;
    private float TimeDiffrence2;
    [HideInInspector]
    public bool AttackAnimationPlaying;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public int GiveDamageToBarberian = 30;
    public bool AttackInterval = true;

    [Range(0,100)] public int MaxHealth = 100;
    [Range(0,100)]public int currentHealth;
    public bool PlayerDied;
    public bool OnLand;
    public bool OnUpland;

    public int coins;
    public Text coinCount;
    public Text ShopCoin;
    public int Potions;
    public int DashSpeed;
    public int SoundCount;
    [HideInInspector]
    public bool matithekeUpore;



    void Awake()
    {
		if(instance == null){
			instance = this;
		}
	}
 
    void Start()
    {
        InstantiateRock = true;
        InstantiateRock2 = true;
        OnUpland = false;
        NowHit = true;
        PlayerDied = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = MaxHealth;
        healthbarscript.SetMaxHealth(MaxHealth);
        AttackAnimationPlaying = false;
        simpleCameraShakeInCinemachine = GameObject.FindObjectOfType<SimpleCameraShakeInCinemachine>();
        joystickControll = GameObject.FindObjectOfType<JoystickControll>();
        
    }

    void FixedUpdate()
    {
        if(currentHealth > 100)
        {
            HealthEqualsTo(100);
        }
        if (HomeUiManager.instance.continueButtonPressed == true)
        {
            PlayerData data = SaveSystem.LoadPlayer();


            HealthEqualsTo(data.healtH);
            coins = data.coin;

            Vector2 position;
            position.x = data.position[0];
            position.y = data.position[1];

            transform.position = position;
            anim.Play("PlayerIdle");
            HomeUiManager.instance.continueButtonPressed = false;
        }
        if(currentHealth<1)
        {
            anim.SetBool("Running",false);
            anim.Play("PlayerDie");
        }
        coinCount.text = coins.ToString();
        ShopCoin.text = coins.ToString();
                
    }



//Attack Button And Hit Effect ................................................................\/

    public void Attack()
    {

//Enemy Damage..................................................................................\/

    if(AttackInterval == true)
    {
        if( PlayerDied == false && Grounded == true)//&& barberian.BarberianDied == true)
        {
                AttackAnimationPlaying = true;
                StartCoroutine("AttackAnimationPlayingInterval");

        //Combo Attack..........................................\/

            comboStep +=1;
            if(comboStep==1)
            {
                comboTime = Time.time;
            }

            if(comboStep==2)
            {
                comboTime2 = Time.time;
                TimeDiffrence = comboTime2 - comboTime;
                if(TimeDiffrence>comboTimeDiffrence){comboStep=0;anim.SetTrigger("Attack");SfxManagerScript.PlayOneStop("SwordSound");}
            }

            if(comboStep==3)
            {
                comboTime3 = Time.time;
                TimeDiffrence2 = comboTime3 - comboTime2;
                if(TimeDiffrence2>comboTimeDiffrence)
                {
                    comboStep=0;
                    anim.SetTrigger("Attack");
                    SfxManagerScript.PlayOneStop("SwordSound");
                }
            }


            if(comboStep==1)
            {
                SfxManagerScript.PlayOneStop("SwordSound");
                anim.SetTrigger("Attack");
            }
                if(comboStep == 2)
            {
                anim.Play("PlayerAttack2");
                SfxManagerScript.PlayOneStop("Sword2Sound");
                TimeDiffrence =0;
            }
                if(comboStep == 3)
            {
                anim.Play("PlayerAttack3");
                SfxManagerScript.PlayOneStop("Sword3Sound");
                TimeDiffrence2=0;
                comboStep=0;
            }

        }
            AttackInterval = false;
            StartCoroutine("AttackIntervaltime"); 

        
     }   
    
    }

    //Hit Enemy.......................\/
    public void GiveDamageToEnemy()
    {
        if(currentHealth > 0 )
            {
            Collider2D[] hitEnemy =  Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,EnemyLayers);

                foreach(Collider2D enemy in hitEnemy)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(GiveDamageToBarberian);
                }
            Collider2D[] hitTower =  Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,TowerLayer);

                foreach(Collider2D enemy in hitTower)
                {
                    enemy.GetComponent<LightBallThrower>().TakeDamage(30);
                }
            }
    }


    IEnumerator AttackIntervaltime()
    {
        
        yield return new WaitForSeconds(0.5f);
        AttackInterval = true;       
    }
    IEnumerator AttackAnimationPlayingInterval()
    {
        yield return new WaitForSeconds(0.3f);
        AttackAnimationPlaying = false;
    }

    void OnDrawGizmosSelected() 
    {
        if(AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position,AttackRange);    
    } 



//Jumping Area.....................................................................\/
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Heart")
        {
            IncreaseHealth(30);
            HealEffect.SetActive(true);
            StartCoroutine("HealEffectDeactive");
        }
        if(other.gameObject.tag == "Land" && currentHealth > 0)
        {
            OnLand = true;
            Grounded = true;
            if(matithekeUpore == false)
            {
                anim.Play("PlayerIdle");
                StartCoroutine("IdleanimationStop");
            }
        }
        if(other.gameObject.tag == "UpLand" && currentHealth > 0)
        {
            OnUpland = true;
            Grounded = true;
            if(matithekeUpore == false)
            {
                anim.Play("PlayerIdle");
                StartCoroutine("IdleanimationStop");
            }
        }
        if(other.gameObject.tag == "Rock")
        {
            if(currentHealth >0){
            TakeDamage(RockDamage);
            simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration;
            die();}
        }
        if(other.gameObject.tag == "Spring")
        {
            if(currentHealth > 0)
            {
            anim.SetTrigger("Jump");
            simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration;
            rb.velocity = new Vector2(0,SpringJumpSpeed);
            }

        }
        if(other.gameObject.tag == "Spike")
        {
            if(currentHealth >0){
            TakeDamage(SpikeDamage);
            simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration;
            die();}
        }
        if(other.gameObject.tag == "Wall")
        {
            if (currentHealth >0){
            TakeDamage(100);
            simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration;
            die();}
        }
         
    }
    public void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "UpLand" && currentHealth > 0)
        {
            OnUpland = false;
            Grounded = false;
        }
    }
    IEnumerator HealEffectDeactive()
    {
        yield return new WaitForSeconds(2f);
        HealEffect.SetActive(false);
    }

    IEnumerator IdleanimationStop()
    {
        yield return new WaitForSeconds(0.001f);
        matithekeUpore = true;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "InstantiateRock" && InstantiateRock == true )
        {
            GameObject rock = Instantiate(Rock,RockIntantiatePosition,Quaternion.identity);
            Destroy(rock,60f);
            InstantiateRock = false;
        }
        if(other.gameObject.tag == "InstantiateRock2" && InstantiateRock2 == true )
        {
            GameObject rock2 = Instantiate(Rock,RockIntantiatePosition2,Quaternion.identity);

            Destroy(rock2,60f);
            InstantiateRock2 = false;
        }
        if(other.gameObject.tag == "Coin" )
        {
            coins++;        
        }
    }

//Damage And Die Area........................................................................\/

     public void TakeDamage(int damage)
    {
        SfxManagerScript.PlayOneStop("PlayerAxeHit");
        if (currentHealth > 0){currentHealth -= damage;}
            
        healthbarscript.SetHealth(currentHealth);
        if(SattackScript.Sattackrunning == false)
        {
        if(PlayerDied == false && currentHealth >= 1 && NowHit == true)
        {
            if(AttackAnimationPlaying == false)
            {
                if(SattackScript.hitAnimationPlay == true)
                {
                    if(Grounded == true)
                    {
                            SoundCount += 1;
                            anim.Play("PlayerHit");
                            if (SoundCount == 1)
                            {
                                SfxManagerScript.PLay("FemalePain1");
                            }
                            if (SoundCount == 2)
                            {
                                SfxManagerScript.PLay("FemalePain2");
                            }
                            if(SoundCount == 2)
                            {
                                SoundCount = 0;
                            }
                    } 
                }
            }
            NowHit = false;
            StartCoroutine("HitAnimationInterval");
        }
        }
        
        Vector2 InstantiatePosition = transform.position;
        InstantiatePosition.y += 1.4f;
        if(currentHealth >= 1)
        {
            GameObject Blood = Instantiate(HitEffect,InstantiatePosition,Quaternion.identity);
            Destroy(Blood,2.0f);
        }

        if(currentHealth <= 0)
        {
            Time.timeScale = 0.4f;
            StartCoroutine("SlowmoStop");
            PlayerDied = true;
            die();
        }
    }

    IEnumerator SlowmoStop()
    {
        yield return new WaitForSeconds(0.50f);
        Time.timeScale = 1f;
    }

    public void IncreaseHealth(int Health)
    {
        currentHealth += Health;
        healthbarscript.SetHealth(currentHealth);
    }
    public void HealthEqualsTo(int Health)
    {
        currentHealth = Health;
        healthbarscript.SetHealth(currentHealth);
    }

    public void die()
    {
        anim.Play("PlayerDie");
        StartCoroutine("DestroyPlayer");
    }

    IEnumerator HitAnimationInterval()
    {
        yield return new WaitForSeconds(1.34f);
        NowHit = true;
    }

    IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(1.28f);
        player.SetActive(true);
        
    }

    public void CameraCut3_Boss()
    {
        bossScript.BossCamera.SetActive(true);
        anim.SetBool("Talk",false);
        bossScript.animator.SetBool("Talk2",true);
    }

    public void SlowMoStart()
    {
        Time.timeScale = 0.12f;
        joystickControll.SlowMOPLaying = true;    
    }
    public void timeScaleReset()
    {
        Time.timeScale = 1;
        joystickControll.SlowMOPLaying = false;    
    }

    public void LightingAnimation()
    {
        LightAnim.SetTrigger("LightAnim");
    }
    public void PlayerDieSoundEffect()
    {
        SfxManagerScript.PLay("PlayerDie");
    }
    public void PlayerAttackSound1()
    {
        SfxManagerScript.PlayOneStop("PlayerAttackSound1");
    }
    public void PlayerAttackSound2()
    {
        SfxManagerScript.PlayOneStop("PlayerAttackSound2");
    }
    public void PlayerAttackSound3()
    {
        SfxManagerScript.PlayOneStop("PlayerAttackSound3");
    }
    public void SpatialAttackSound()
    {
        SfxManagerScript.PlayOneStop("SpatialAttackSound");
    }



}


