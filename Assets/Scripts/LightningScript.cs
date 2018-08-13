using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour {


    public Light mylight;
    public float flashtimer = 1f;
    private float on;
    private float off;
    public float offTimerStart = 5f;
    public float offTimerEnd = 15f;
    public bool flashOn = false;
    private bool thunderPlaying = false;

    private void Start()
    {
        on = flashtimer;
        off = Random.Range(offTimerStart, offTimerEnd);
    }

    void Update()
    {
        if (flashOn)
        {
            flashtimer -= Time.deltaTime;
            if (flashtimer < 0)
            {
                flashOn = false;
                flashtimer = on;
            }
        }
        else
        {
            off -= Time.deltaTime;
            if (off < 0)
            {
                flashOn = true;
                thunderPlaying = false;
                off = Random.Range(offTimerStart, offTimerEnd);
            }
        }

        if (flashOn)
        {
            mylight.enabled = true;
        }
        else
        {
            mylight.enabled = false;
        }

        if (flashOn && !thunderPlaying)
        {
            
            int ran = Random.Range(1, 4);
            string thunderString = "thunder" + ran;
            Debug.Log(thunderString);
            thunderPlaying = true;
            FindObjectOfType<AudioManager>().Play(thunderString);
        }
        
    }
}
