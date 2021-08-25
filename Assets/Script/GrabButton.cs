using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrabButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public Collider2D Statuecollider;
    public Rigidbody2D StatueRB;
    private RigidbodyConstraints2D originalConstraints;
    void Start()
    {
        originalConstraints = StatueRB.constraints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Statuecollider.isTrigger = false;
        
        StatueRB.constraints = originalConstraints;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Statuecollider.isTrigger = true;
        StatueRB.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
