using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sattack : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public GameObject SattackEffect;
    public GameObject Player;
    public Player PlayerScript;
    private SimpleCameraShakeInCinemachine simpleCameraShakeInCinemachine;
    Vector3 PlayerScale;
    Vector3 lastposition;
    public Animator anim;
    public Animator Playeranimator;
    //public Left left;
    //public bool start = false;
    public float instantiatePositionX;
    public float instantiatePositionY;
    public float instantiatePositionZ;
    
    public  bool Instantiat = true;


    public Transform SAttackPoint;
    public float SAttackRange;
    public int SAttackDamage = 100;
    public LayerMask EnemyLayers;
    [HideInInspector]
    public bool Sattackrunning = false;
    [HideInInspector]
    public bool hitAnimationPlay = true;

     // Start is called before the first frame update
    void Start()
    {
        simpleCameraShakeInCinemachine = GameObject.FindObjectOfType<SimpleCameraShakeInCinemachine>();
    }

    // Update is called once per frame
    void Update()
    {
        

        PlayerScale = Player.transform.localScale;
        lastposition = Player.transform.position;
        if(PlayerScale.x == 0.13f)
        {
            lastposition.x += instantiatePositionX;
            lastposition.y += instantiatePositionY;
            lastposition.z += instantiatePositionZ;
        }
        else if(PlayerScale.x == -0.13f)
        {
            lastposition.x += -instantiatePositionX;
            lastposition.y += instantiatePositionY;
            lastposition.z += instantiatePositionZ;
        }
        
    }

    void OnDrawGizmosSelected() 
    {
        if(SAttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(SAttackPoint.position,SAttackRange);    
    }

    IEnumerator NichePorleBarberianMorbe1sPor()
    {
        yield return new WaitForSeconds(1f);
        if(PlayerScript.currentHealth > 0)
        {
            simpleCameraShakeInCinemachine.ShakeElapsedTime = simpleCameraShakeInCinemachine.ShakeDuration;
            Collider2D[] hitEnemy =  Physics2D.OverlapCircleAll(SAttackPoint.position,SAttackRange,EnemyLayers);

            foreach(Collider2D enemy in hitEnemy)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(SAttackDamage);
                }
        }
    }
    IEnumerator LightChange()
    {
        yield return new WaitForSeconds(5f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("NichePorleBarberianMorbe1sPor");
        StartCoroutine("LightChange");

        if(Instantiat == true )
        {
            Playeranimator.SetBool("Sattack",true);
            Sattackrunning = true;
            hitAnimationPlay = false;
        }
        if(Instantiat == true )
        {
            if(PlayerScale.x == 0.13f)
            {
                StartCoroutine("InstantiateAfterTime");
            }
            else if(PlayerScale.x == -0.13f)
            {
                StartCoroutine("InstantiateAfterTime");
            }
        }

        
    }
    IEnumerator InstantiateAfterTime()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(SattackEffect,Player.transform.position,Quaternion.identity);

    }
    IEnumerator HitanimationPlay()
    {
        yield return new WaitForSeconds(.35f);
        hitAnimationPlay = true;

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Playeranimator.SetBool("Sattack",false);
        if(Instantiat == true)
          {
            Sattackrunning = false;
            Instantiat = false;
            anim.SetTrigger("ColourChange");
            StartCoroutine("SeffectMiddleDelay");
        
          }
        
    }

    IEnumerator SeffectMiddleDelay()
    {
        yield return new WaitForSeconds(15f);
        Instantiat = true;
    } 
    
}
