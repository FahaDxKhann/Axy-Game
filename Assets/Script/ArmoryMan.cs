using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryMan : Enemy
{
    Animator animator;
    public GameObject Heart;
    bool InstanHeart = true;

    public Transform pointA,pointB;
    public bool Switch;
    public float switchTime=10f;



    public override void Start()
    {
        base.Start();
        Switch = false;
        animator = GetComponent<Animator>();
        
    }
    public override void FixedUpdate() 
    {
        base.FixedUpdate();

        if(playerScript.PlayerDied == true)
        {
            anim.Play("Idle");
            anim.SetBool("Walk",false);
            anim.SetBool("Attack",false);
        }
        if(BarberianDied == true)
        {
            if(InstanHeart == true)
            {
            Vector2 Pos = transform.position;
            Pos.y += 1;
            Instantiate(Heart,Pos,Quaternion.identity);
            InstanHeart = false;
            }
        }
        if(BarberianDied == false)
        {
            if(enemyFollowingPlayer == false)
            {
                Movement();
            }
            if(enemyFollowingPlayer == true)
            {
                animator.SetBool("Wallk",false);
            }   
        }  
    }


    public void Movement()
    {
        if(Switch == false)
        {
            StartCoroutine("SwithcNow");
        }
        if(Switch == true)
        {
            StartCoroutine("SwithcNow2");
        }
        if(Switch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position,pointB.transform.position,0.5f*Time.deltaTime);
            Vector2 Scale = this.transform.localScale;
            Scale.x = 0.18f;
            this.transform.localScale = Scale;
            animator.SetBool("Wallk",true);
        }
        if(Switch == true)
        {
            transform.position = Vector3.MoveTowards(transform.position,pointA.transform.position,0.5f*Time.deltaTime);
            Vector2 Scale = this.transform.localScale;
            Scale.x = -0.18f;
            this.transform.localScale = Scale;
            animator.SetBool("Wallk",true);
        }
    }

    IEnumerator SwithcNow()
    {
        yield return new WaitForSeconds(switchTime);
        Switch = true;
    }
    IEnumerator SwithcNow2()
    {
        yield return new WaitForSeconds(switchTime);
        Switch = false;
    }
}
