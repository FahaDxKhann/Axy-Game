using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public Animator animator;
    private Vector2 FoxScale;
    public float foxScale;
    public float FoxLastPos;
    public float Speed;
    public float DistanceToNotFollow = 2;
    public float EnemyDetectRange = 8;
    public float GiveDamageRange = 1;
    public LayerMask EnemyLayers;


    public Player PlayerScript;
    public Transform PlayerPos;
    public Shop ShopScript;
    public Transform PlayerHealthEffectInstanPos;
    public Healthbar healthbarscript;
    public GameObject HealthEffect;
    public bool IncreseHealth;
    public bool NowAttack,giveDamage;
    public int giveDamageToEnemyAmount;


    public void Start() 
    {
        giveDamage = true;
        IncreseHealth = true;
        NowAttack = false; 
        FoxLastPos = transform.position.x;   
    }
    public void FixedUpdate()
    {
        if(ShopScript.levelThreeRunning == false)
        {
            follow();
        }
        
                

        if(ShopScript.levelOneRunning == true)
        {
            LevelOne();
        }
        if(ShopScript.levelTwoRunning == true)
        {
            LevelTwo();
        }

        if(ShopScript.levelThreeRunning == true)
        {
            LevelThree();
        }
        
        if(ShopScript.levelFourRunning == true)
        {
            LevelFour();
        }     
        
    }




    public void follow()
    {
        float distenceBetwenPlayerAndPet = Vector2.Distance(PlayerPos.transform.position,transform.position);
        if(distenceBetwenPlayerAndPet > DistanceToNotFollow)
        {
                if(PlayerPos.transform.position.x <= transform.position.x)
            {
                FoxScale = transform.localScale;
                FoxScale.x = -foxScale;
                transform.localScale = FoxScale;
            }
            else if(PlayerPos.transform.position.x >= transform.position.x)
            {
                FoxScale = transform.localScale;
                FoxScale.x = foxScale;
                transform.localScale = FoxScale;
            }
        animator.SetBool("Run",true);
        transform.position = Vector2.MoveTowards(this.transform.position,PlayerPos.position,Speed*Time.deltaTime);
        }
        else if(distenceBetwenPlayerAndPet < DistanceToNotFollow )
        {
            animator.SetBool("Run",false);
        }
        
    }




//Level One......................
    public void LevelOne()
    {
        if(IncreseHealth == true)
        {
            if(PlayerScript.currentHealth <= 97 && PlayerScript.currentHealth >= 0 )
            {
                PlayerScript.IncreaseHealth(3);
                GameObject Effect = Instantiate(HealthEffect,PlayerHealthEffectInstanPos.transform.position,Quaternion.Euler(-90,0,0));
                Destroy(Effect,3f);
            }
            IncreseHealth = false;
            StartCoroutine("IncreAseHealthNow");
        }
    }
    IEnumerator IncreAseHealthNow()
    {
        yield return new WaitForSeconds(120f);
        IncreseHealth = true;
    }



//Level Two.............
    public void LevelTwo()
    {
        if(IncreseHealth == true)
        {
            if(PlayerScript.currentHealth <= 96 && PlayerScript.currentHealth >= 0 )
            {
                PlayerScript.IncreaseHealth(4);
                GameObject Effect = Instantiate(HealthEffect,PlayerHealthEffectInstanPos.transform.position,Quaternion.Euler(-90,0,0));
                Destroy(Effect,3f);
            }
            IncreseHealth = false;
            StartCoroutine("IncreAseHealthNowLvl2");
        }
    }
    IEnumerator IncreAseHealthNowLvl2()
    {
        yield return new WaitForSeconds(90f);
        IncreseHealth = true;
    }



    public void LevelThree()
    {
        if(NowAttack == false)
        {
            follow();

        }

        Collider2D[] hitEnemy =  Physics2D.OverlapCircleAll(this.transform.position,EnemyDetectRange,EnemyLayers);

            foreach(Collider2D enemy in hitEnemy)
                {
                    if(enemy.GetComponent<Enemy>().enemyFollowingPlayer == true && enemy.GetComponent<Enemy>().BarberianDied == false)
                    {
                        if(NowAttack == false)
                        {
                        transform.position = Vector2.MoveTowards(this.transform.position,enemy.transform.position,2*Time.deltaTime);
                        animator.SetBool("Run",true);
                        }
                        if(FoxLastPos <transform.position.x)
                        {
                            FoxScale = transform.localScale;
                            FoxScale.x = -foxScale ;
                            FoxScale = transform.position;
                        }
                        if(FoxLastPos >transform.position.x)
                        {
                            FoxScale = transform.localScale;
                            FoxScale.x = foxScale ;
                            FoxScale = transform.position;
                        }
                        
                    }
                    if(Vector2.Distance(transform.position,enemy.transform.position) < 2f && enemy.GetComponent<Enemy>().BarberianDied == false)
                    {
                        NowAttack = true;
                        if(NowAttack == true)
                        {
                            animator.SetTrigger("Attack");
                        }                       
                        StartCoroutine("StopFollowingPlayerForAwhile");
                    }    
            if(giveDamage == true  && enemy.GetComponent<Enemy>().BarberianDied == false)
            {
            Collider2D[] DamageEnemy =  Physics2D.OverlapCircleAll(this.transform.position,GiveDamageRange,EnemyLayers);

                foreach(Collider2D enemys in DamageEnemy)
                    {
                        enemy.GetComponent<Enemy>().TakeDamageNoEffect(giveDamageToEnemyAmount);
                    }
                giveDamage = false;
                StartCoroutine("GiveDamageAfterFewSeconds");
            }
        }

    }
    IEnumerator StopFollowingPlayerForAwhile()
    {
        yield return new WaitForSeconds(1f);
        NowAttack = false;
    }
    IEnumerator GiveDamageAfterFewSeconds()
    {
        yield return new WaitForSeconds(1f);
        giveDamage = true;
    }




//Level Fouor..............
     public void LevelFour()
    {
        if(IncreseHealth == true)
        {
            if(PlayerScript.currentHealth <= 96 && PlayerScript.currentHealth >= 0 )
            {
                PlayerScript.IncreaseHealth(4);
                GameObject Effect = Instantiate(HealthEffect,PlayerHealthEffectInstanPos.transform.position,Quaternion.Euler(-90,0,0));
                Destroy(Effect,3f);
            }
            IncreseHealth = false;
            StartCoroutine("IncreAseHealthNowLvl4");
        }
    }
    IEnumerator IncreAseHealthNowLvl4()
    {
        yield return new WaitForSeconds(60f);
        IncreseHealth = true;
    }




    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position,EnemyDetectRange);
        Gizmos.DrawWireSphere(this.transform.position,GiveDamageRange);
        
    }
    
}
