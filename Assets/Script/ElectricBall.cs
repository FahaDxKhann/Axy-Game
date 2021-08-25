using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    GameObject ElectricBallThrowPosition;
    public GameObject Explosion;
    GameObject Player;
    Player PlayerScript;
    public float speed;
    public int ElectrickBallDamage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ElectricBallThrowPosition = GameObject.FindGameObjectWithTag("ElectricBallThrowPosition");
        Player = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDirection = (ElectricBallThrowPosition.transform.position - transform.position).normalized*speed;
        rb.velocity = new Vector2(moveDirection.x,moveDirection.y);
        Destroy(this.gameObject,7f);
        SfxManager.instance.PLay("MagicBall");
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            SfxManager.instance.PLay("MagicBallExplosion");
            Destroy(this.gameObject);
            PlayerScript.TakeDamage(ElectrickBallDamage);
            GameObject explo = Instantiate(Explosion,this.transform.position,Quaternion.identity);
            Destroy(explo.gameObject,2f);
            
        }
        if(other.gameObject.tag == "Land")
        {
            SfxManager.instance.PLay("MagicBallExplosion");
            Destroy(this.gameObject);
            GameObject explo = Instantiate(Explosion,this.transform.position,Quaternion.identity);
            Destroy(explo.gameObject,2f);
            
        }  
    }

    
}
