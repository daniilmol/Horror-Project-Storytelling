using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    /*Notes: 
     * Check loop is on/off, playOnAwake is on/off
     * Use it by :  AudioController.aCtrl.GetSound("bgMusic");
     * Make sure AudioController script run before other scripts
     */

    // Player Sound
    [Header("Player Sound")]
    public AudioSource PlayerWalkingSound;

    // Interact Sound
    [Header("Interact Sound")]
    public AudioSource CollectSoulSound;



    // AudioController
    public static AudioController aCtrl;

    // volume
    private float volume = 1.0f;
    public void Awake()
    {
        if (aCtrl == null)
        {
            DontDestroyOnLoad(gameObject);
            aCtrl = this;
        }
    }

    public void Update()
    {
        SetVolume(this.volume);
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }

    public void PlaySound(AudioSource sound)
    {
        if (sound != null)
        {
            sound.Play();
        }
    }
    public void StopSound(AudioSource sound)
    {
        if (sound != null)
        {
            sound.Stop();
        }
    }


    public AudioSource GetSound(string sound)
    {
        switch (sound)
        {
            //case "bgMusic":
            //    // bgMusic
            //    return bgMusic;
            //
            
            // Player
            case "playerWalking":
                return PlayerWalkingSound;

            // Interact
            case "soulCollecting":
                return CollectSoulSound;
            default:
                Debug.Log("Error, check PlaySound(string) in AudioController");
                return null;
        }
    }


}
