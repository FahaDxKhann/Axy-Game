using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Animator KeyAnimator;
    public GameObject KeyEffect;
    public bool KeyCollected;

    private void Start() {
        KeyCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            KeyAnimator.Play("Vanish");
            GameObject keyeffect = Instantiate(KeyEffect,this.transform.position,Quaternion.identity);
            Destroy(keyeffect,0.40f);
            KeyCollected = true;
            Destroy(this.gameObject,0.40f);

        }
    }
}
