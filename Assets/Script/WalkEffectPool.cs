using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEffectPool : MonoBehaviour
{

    public static WalkEffectPool WalkEffectPoolInstanse;

    [SerializeField]

    private GameObject pooledWalkEffect;
    private int pooledAmount = 7;
    public bool notEnoughWalkEffectInPool;

    private List<GameObject> WalkEffects;

    private void Awake()
    {
        WalkEffectPoolInstanse = this;
    }
    private void Start() 
    {
        WalkEffects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject effect = Instantiate(pooledWalkEffect);
            effect.SetActive(false);
            WalkEffects.Add(effect);
        }
    }
    // Start is called before the first frame update
    public GameObject getPooledeffect()
    {
        for (int i = 0; i < WalkEffects.Count; i++)
        {
            if (!WalkEffects[i].activeInHierarchy)
            {
                return WalkEffects[i];
            }
        }
        if (notEnoughWalkEffectInPool)
        {
            GameObject effect = Instantiate(pooledWalkEffect);
            WalkEffects.Add(effect);
            return effect;
        }
            return null;
    }
}
