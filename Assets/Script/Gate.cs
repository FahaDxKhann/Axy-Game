using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public SfxManager SfxManagerScript;
    Animator animator;
    public GameObject Key1;
    public Key KeyScript;
    public Vector2 KeyInstanPos;
    public GameObject FindtheKeyText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            if(KeyScript.KeyCollected == true)
            {
                animator.Play("Open");
            }
            else if(KeyScript.KeyCollected == false)
            {
                Key1.SetActive(true);
                FindtheKeyText.SetActive(true);
                StartCoroutine("DisableFindKeyText");
                SfxManagerScript.PLay("FindTheKeySound");
            }
        }
    }
    IEnumerator DisableFindKeyText()
    {
        yield return new WaitForSeconds(4f);
        FindtheKeyText.SetActive(false);
    }
    public void GateOpenSound()
    {
        SfxManager.instance.PLay("GateOpen");
    }
    

}
