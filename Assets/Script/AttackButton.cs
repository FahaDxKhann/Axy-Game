using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public Player Player;
    void Start()
    {
        
    }
    void Update()
    {
    
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        //Player.AttackAnimationPlaying = false;
    }
}