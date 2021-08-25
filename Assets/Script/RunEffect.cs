using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEffect : MonoBehaviour
{
    private void OnEnable() 
    {
        Invoke("Disable", 1.5f);
    }

    void Disable() 
    {
        gameObject.SetActive(false);
    }
    private void OnDisable() 
    {
        CancelInvoke();
    }
}
