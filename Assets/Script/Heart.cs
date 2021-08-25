using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    GameObject Player;
    public Player PLayeScript;
    public GameObject HealthEffect;
    public Healthbar healthbarscript;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            SfxManager.instance.PLay("HealSound");
        }
    }
}
