using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pet : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    private Vector2 PetScale;
    public float petScale;
    public float Speed;
    public float DistanceToNotFollow = 2;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        float distenceBetwenPlayerAndPet = Vector2.Distance(player.transform.position,transform.position);
        if(distenceBetwenPlayerAndPet > DistanceToNotFollow)
        {
            follow();
        }
        else if(distenceBetwenPlayerAndPet < DistanceToNotFollow)
        {
            animator.SetBool("Run",false);
        }
    }

    public void follow()
    {
        if(player.transform.position.x <= transform.position.x)
            {
                PetScale = transform.localScale;
                PetScale.x = -petScale;
                transform.localScale = PetScale;
            }
            else if(player.transform.position.x >= transform.position.x)
            {
                PetScale = transform.localScale;
                PetScale.x = petScale;
                transform.localScale = PetScale;
            }
        animator.SetBool("Run",true);
        transform.position = Vector2.MoveTowards(this.transform.position,player.position,Speed*Time.deltaTime);
    }
}
