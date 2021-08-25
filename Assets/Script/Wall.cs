using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Animator animator;
    public bool nowStart;
    // Start is called before the first frame update
    void Start()
    {
        nowStart = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(nowStart == true)
        {
            animator.SetBool("Start",true);
        }
    }
}
