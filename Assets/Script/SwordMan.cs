using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Enemy
{
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(playerScript.PlayerDied == true)
        {
            anim.Play("Idle");
            anim.SetBool("Walk",false);
            anim.SetBool("Attack",false);
        }
    }
}
