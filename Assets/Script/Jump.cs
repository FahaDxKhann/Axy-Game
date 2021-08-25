using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public Player Player;
    public SfxManager SfxManagerScript;
    public JoystickControll joystickControllScript;
    public float JumpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Grounded == false)
        {
            SfxManagerScript.Stop("RunSound");
            joystickControllScript.RunSoundPlaying = false;
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Player.Grounded == true && Player.PlayerDied == false)
        {
            SfxManager.instance.PLay("PlayerJump");
            Player.anim.SetTrigger("Jump");
            Player.rb.velocity = new Vector2(0,JumpSpeed);
            Player.Grounded = false;
            Player.matithekeUpore = false;
            Player.OnLand = false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
