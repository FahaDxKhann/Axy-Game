using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dash : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public Player player;
    public Transform PlayerTransform;
    Vector2 localScale;
    int LocalScaleX;
    int DashSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        localScale = PlayerTransform.transform.localScale;
        localScale.x = LocalScaleX;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (LocalScaleX == 0.13f)
        {
            player.rb.AddForce(new Vector2(DashSpeed,0f));
        }
        if (LocalScaleX == -0.13f)
        {
            player.rb.AddForce(new Vector2(-DashSpeed,0f));
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
