using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberianSit : Enemy
{

    public override void Start()
    {
        base.Start();
        
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if(playerScript.PlayerDied == true)
        {
            anim.Play("Standing");
            anim.SetBool("Walk",false);
            anim.SetBool("Attack",false);
        }
    }
}
