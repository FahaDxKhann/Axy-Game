using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Left : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    [SerializeField] Vector3 Currentscale;
    public GameObject Player;
    public bool pressed;
    public float Walkspeed;
    public float Runspeed;
    public Animator anim;
    //public bool pressed3s =false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed )//& pressed3s==false)
        {
            Vector3 Currentscale = Player.transform.localScale;
            Currentscale.x = -0.13f;
            Player.transform.localScale = Currentscale;
            Player.transform.Translate(-Runspeed*Time.deltaTime,0f,0f);
        }
        /*else if (pressed & pressed3s==true)
        {
            anim.SetBool("Running",true);
            Player.transform.Translate(-Runspeed*Time.deltaTime,0f,0f);
        }*/
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        //StartCoroutine("run");       
        pressed = true;
        anim.SetBool("Running",true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //StopCoroutine("run");
        
        pressed = false;
        //pressed3s = false;
        //anim.SetBool("Walking",false);
        anim.SetBool("Running",false);
    }
    

    /*IEnumerator run()
    {
        
            yield return new WaitForSeconds(1f);
            pressed3s = true;    
    }*/
}
