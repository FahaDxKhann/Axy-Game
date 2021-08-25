using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTriggerScript : MonoBehaviour
{
    public GameObject LeverText;
    Lever LeverScript;
    private void Start() 
    {
        LeverScript = GameObject.FindObjectOfType<Lever>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(LeverScript.AnimationPlayedOnce == false)
            {
            LeverText.SetActive(true);
            StartCoroutine("LeverTextOfftime");
            }
        }
    }


    IEnumerator LeverTextOfftime()
    {
        yield return new WaitForSeconds(4f);
        LeverText.SetActive(false);
    }   
    
}
