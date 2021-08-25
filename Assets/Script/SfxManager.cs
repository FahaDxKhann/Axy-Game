using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class SfxManager : MonoBehaviour
{
    public static SfxManager instance;
    public Sound[] sounds;
    private void Awake() 
    {
		if(instance == null){
			instance = this;
		}
	
        foreach(Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.volume;
            s.Source.pitch = s.pitch;
            s.Source.loop = s.loop;
        }   
    }

    public void PLay(string name)
    {
        Sound s = Array.Find(sounds,Sound=> Sound.Name == name);
        s.Source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds,Sound=> Sound.Name == name);
        s.Source.Stop();
    }
    public void PlayOneStop(string name)
    {
        Sound s = Array.Find(sounds,Sound=> Sound.Name == name);
        s.Source.PlayOneShot(s.Clip,s.volume);
    }
   



}
