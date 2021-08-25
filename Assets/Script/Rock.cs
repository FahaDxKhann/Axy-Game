using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    bool nowStop = false;
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Land" && nowStop == false)
        {
            SfxManager.instance.PLay("RockRollingSound");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "StopRockSound")
        {
            nowStop = true;
            SfxManager.instance.Stop("RockRollingSound");
        }
    }
}
