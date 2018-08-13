using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    //public Slider soundSlider, sfxSlider;

    // Use this for initialization
    void Awake ()
    { 
		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;

           /* if(s.name == "BG")
            {
                soundSlider.value = PlayerPrefs.GetFloat("SoundValue",1);
                

            }
            else
            {
                sfxSlider.value = PlayerPrefs.GetFloat("SFXValue", 1);
            }*/
        } 
	}
	
	public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Update()
    {
       /* foreach (Sound s in sounds)
        {
            if (s.name == "BG")
            {
                s.source.volume = soundSlider.value;
                PlayerPrefs.SetFloat("SoundValue", soundSlider.value);
            }
            else
            {
                s.source.volume = sfxSlider.value;
                PlayerPrefs.SetFloat("SFXValue", sfxSlider.value);
            }

            PlayerPrefs.Save();
        }*/
    }
}
