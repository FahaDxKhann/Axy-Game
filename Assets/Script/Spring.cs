using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Player PLayerScript;
    public GameObject SpringEffect;
    public SfxManager SfxManagerScript;
    Animator animator;

    private void Update() {
        if(PLayerScript.PlayerDied == true)
        {
            return;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && PLayerScript.PlayerDied == false)
        {
            SfxManagerScript.PlayOneStop("SpringSound");
            GameObject SpringEffects = Instantiate(SpringEffect,transform.position,Quaternion.identity);
            Destroy(SpringEffects,2f);

        }   
    }
}
