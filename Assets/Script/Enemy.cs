using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Player playerScript;
    public JoystickControll JoystickScript;
    private SimpleCameraShakeInCinemachine simpleCameraShakeInCinemachine;
    public SfxManager sfxManagerScript;
    public GameObject HitEffect;
    public GameObject DieEffect;
    public GameObject QuestionMark;
    public float HitEffectInstanPosition;
    public float CoinInstanPositionY;
    public Vector2 CoinInstantiatePosition;
    Vector2 MaxCoinInstanPos;
    Vector2 BloodInstantiatePosition;
    public GameObject coin;
    private Transform Player;
    public GameObject enemy;
    public float enemyscale;
    [HideInInspector]
    public Animator anim;
    public float Distance;
    private Vector3 EnemyScale;
    [SerializeField]protected float Speed;
    
    public int MaxHealth = 100;
    public int currentHealth;
    public bool BarberianDied;
    public bool enemyFollowingPlayer;

    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public int AttackDamage = 0;
    public bool AttackInterval = true;
    bool Attacking;
    public LayerMask PlayerLayer;

    bool hitAnimPlay = true;
    public float SlomoScale = 0.19f;
    public bool PlayedOnce;
    public static Enemy instance;


    void Awake(){
		if(instance == null){
			instance = this;
		}
	}

    public virtual void Start() 
    {
        simpleCameraShakeInCinemachine = GameObject.FindObjectOfType<SimpleCameraShakeInCinemachine>();
        BarberianDied = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = gameObject.GetComponent<Animator>();
        currentHealth = MaxHealth;
        enemyFollowingPlayer = false;
        Attacking = false;
        
    }


    public virtual void FixedUpdate()
    {
        float DistanceBetweenPlayerAndEnemy = Vector2.Distance(Player.position,transform.position);

        if( BarberianDied == true)
        {
            return;
        }

//PLAYER R ENEMY JOKHON MUKHOMUKHI BA DURE TOKHON....................................................................\/

        if(DistanceBetweenPlayerAndEnemy >= 1.3f && BarberianDied == false)
            {
                anim.SetBool("Attack",false);
                if(playerScript.PlayerDied == false)
                {
                    if(Attacking == false)
                    {
                        Follow();
                    }            
                }
            }
        else if(DistanceBetweenPlayerAndEnemy <= 1.3f && BarberianDied == false)
            {
                if( BarberianDied == false && currentHealth >= 0)
                {
                    anim.SetBool("Attack",true);
                    if(currentHealth > 0f){QuestionMark.SetActive(false);}                
                }
            }
//Question Mark Show Option.........................
        if(DistanceBetweenPlayerAndEnemy <= 7 && BarberianDied == false)
            {
                if(currentHealth > 0f){QuestionMark.SetActive(true);}                             
            }
        if(DistanceBetweenPlayerAndEnemy >= 7 && BarberianDied == false)
            {
                if(currentHealth > 0f){QuestionMark.SetActive(false);}       
            }
    }

    //Attack KORE PLAYER KE DAMAGE DEYA............................................................\/
    public void GiveDamageToPlayer()
    {
        float DistanceBetweenPlayerAndEnemy = Vector2.Distance(Player.position,transform.position);
         
            if(playerScript.currentHealth > 1 && DistanceBetweenPlayerAndEnemy <= 1.3f)
            {

                Collider2D[] hitPlayer =  Physics2D.OverlapCircleAll(AttackPoint.position,AttackRange,PlayerLayer);

                foreach(Collider2D Player in hitPlayer)
                    {
                        
                            playerScript.TakeDamage(AttackDamage);
                        
                    }
                
            }       

    }

//Duita Attack er majh khaner time..................................\/
    IEnumerator AttackIntervalTime()
    {
        yield return new WaitForSeconds(1f);
        AttackInterval = true;
    }



//PLAYER RANGE ER VITOR ASHLE TAKE FOLLOW KORA.......................................................\/
    public void Follow()
    {
        float DistanceBetweenPlayerAndEnemy = Vector2.Distance(Player.position,transform.position);
        if(DistanceBetweenPlayerAndEnemy < Distance && BarberianDied == false)
        {
            if(Player.transform.position.x <= transform.position.x)
            {
                EnemyScale = enemy.transform.localScale;
                EnemyScale.x = -enemyscale;
                enemy.transform.localScale = EnemyScale;
            }
            else if(Player.transform.position.x >= transform.position.x)
            {
                EnemyScale = enemy.transform.localScale;
                EnemyScale.x = enemyscale;
                enemy.transform.localScale = EnemyScale;
            }
            anim.SetBool("Walk",true);
            enemyFollowingPlayer = true;
            transform.position = Vector2.MoveTowards(this.transform.position,Player.position,Speed*Time.deltaTime);
            if(currentHealth > 0f){QuestionMark.SetActive(false);}
        }
        else if(DistanceBetweenPlayerAndEnemy > Distance && BarberianDied == false)
        {
           anim.SetBool("Walk",false);
           enemyFollowingPlayer = false;
        }
    }


//BARBERIAN ER JONNO BANNU POINT A EKTA CIRCLE AKANO..................................\/ 
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position,Distance);

        if(AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position,AttackRange); 
    }



//JOKHON ATTACK KORBE PLAYER TOKHON DAMAGE HOWA AND MARA JAWA..................................\/
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0){simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration;} 
        currentHealth -= damage;
        if(BarberianDied == false)
            {
                if(hitAnimPlay == true)
                {
                    anim.Play("Hit");
                    sfxManagerScript.PlayOneStop("Malehurt");
                }               
            }

        BloodInstantiatePosition = transform.position;
        BloodInstantiatePosition.y += HitEffectInstanPosition;
        if(currentHealth >= 1)
        {
            GameObject Blood = Instantiate(HitEffect,BloodInstantiatePosition,Quaternion.identity);
            Destroy(Blood,3.0f);
        }

    
        if(currentHealth <= 0)
        {
            BarberianDied = true;
            QuestionMark.SetActive(false);
            die();
        }
        hitAnimPlay = false;
        StartCoroutine("HitAnimationPlay");
    }

    public void TakeDamageNoEffect(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            BarberianDied = true;
            QuestionMark.SetActive(false);
            die();
        }
    }

    public void die()
    {
        StartCoroutine("DestroyEnemy");
        StartCoroutine("DieEffectInstantiate");
    }


    public void SlowmoStop()
    {
        Time.timeScale = 1f;
        JoystickScript.SlowMOPLaying = false;
    }

    public void SlowMoStart()
    {
        Time.timeScale = SlomoScale;
        JoystickScript.SlowMOPLaying = true;
    }
    IEnumerator DestroyEnemy()
    {
        anim.Play("Die");
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        CoinInstantiatePosition = transform.position;
        CoinInstantiatePosition.y += CoinInstanPositionY;
        for(int i =0; i<4; i++)
        {
        GameObject CoinAfterDeath =  Instantiate(coin,CoinInstantiatePosition,Quaternion.identity);
        Destroy(CoinAfterDeath,20f);
        }
        
    }
    IEnumerator DieEffectInstantiate()
    {
        Vector2 InstantiatePosition = transform.position;
        InstantiatePosition.y += 0.1f; 
        yield return new WaitForSeconds(1.8f);
        GameObject dieEffect = Instantiate(DieEffect,InstantiatePosition,Quaternion.Euler(90,0,0));
        Destroy(dieEffect,2f);
    }
    IEnumerator HitAnimationPlay()
    { 
        yield return new WaitForSeconds(1f);
        hitAnimPlay = true;
        
    }

    public void PlayDeathSound1()
    {
        if (PlayedOnce == true)
        {
            sfxManagerScript.PLay("MaleDeath1");
            PlayedOnce = false;
        }
        if (PlayedOnce == false)
        {
            sfxManagerScript.PLay("MaleDeath2");
            PlayedOnce = true;
        }
    }
    public void attackSound()
    {
        sfxManagerScript.PLay("SwordSlash");
    }
    public void AxeSound()
    {
        sfxManagerScript.PLay("SwordSound");
    }

}



