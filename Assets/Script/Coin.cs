using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    Animator animator;
    public AudioSource CoinSound;
    public AudioClip coin;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CoinSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            CoinSound.PlayOneShot(coin,0.7f);
            animator.Play("CoinVanish");
            Destroy(this.gameObject,0.45f);

        }   
    }
}
