using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBallThrower : MonoBehaviour
{
    public GameObject PLayer;
    Animator Animator;
    public GameObject ElectricBall;
    public GameObject LightHousePrefab;
    public GameObject Hiteffect;
    public GameObject ExplosionEffect;
    public Vector2 HiteffectInstancePos;
    public float detectArea;
    public Vector2 PrefabInstanPos;
    public Vector2 BallInstanPos;
    public int Speed;
    public bool instanNow;
    public int MaxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        instanNow = true;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(instanNow == true)
        {
            Animator = GetComponent<Animator>();
            InstanAndThrow();
            instanNow = false;
            StartCoroutine("NowInstatiate");
        }   
    }


    public void InstanAndThrow()
    {
        float Distance = Vector2.Distance(PLayer.transform.position,transform.position);
        if(Distance <= detectArea)
        {
            //Vector2 InstanPos = transform.position;
            //InstanPos.y += 0.94f;
            //InstanPos.x += 0.06f;
            GameObject Ball = Instantiate(ElectricBall,BallInstanPos,Quaternion.identity);
        }
    }
    IEnumerator NowInstatiate()
    {
        yield return new WaitForSeconds(2);
        instanNow = true;
    }

    public void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(this.transform.position,detectArea);
    }

    public void TakeDamage(int damage)
    {
        Animator.SetTrigger("Hit");
        GameObject hiteffect = Instantiate(Hiteffect,HiteffectInstancePos,Quaternion.identity);
        Destroy(hiteffect.gameObject,0.4f);
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            GameObject explosion = Instantiate(ExplosionEffect,this.transform.position,Quaternion.identity);
            Destroy(explosion,2f);
            Instantiate(LightHousePrefab,PrefabInstanPos,Quaternion.identity);
            SfxManager.instance.PLay("ExplodeSoundEffect");


        }
    }
    public void HitSound()
    {
        SfxManager.instance.PLay("WoodHit");
    }
}
