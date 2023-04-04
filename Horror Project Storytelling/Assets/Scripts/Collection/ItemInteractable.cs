using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemInteractable : Interactable
{
    //private AudioController aCtrl;
    private GameObject player;
    private GameObject manager;
    public int totalSouls;



    public void Start()
    {
        // aCtrl = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }


    public override void OnFocus()
    {
        // when you look at the gameobject, play sound or smt
        if (gameObject.tag == "Soul")
        {
            manager.GetComponent<EventManager>().HoverScript("Press E to collect Soul");
        }

        if (gameObject.tag == "Pedestal")
        {
            manager.GetComponent<EventManager>().HoverScript("Press E to deposit Souls ");
        }


    }

    public override void OnLoseFocus()
    {
    }

    public override void OnInteract()
    {
        // Interact with the soul shards
        if (gameObject.tag == "Soul")
        {
            // destroy obj
            Destroy(gameObject);

            // play sound - soul collected sound
            manager.GetComponent<AudioManager>().getShard();

            // Add shards collection number
            int currentSoul = player.GetComponent<PlayerStats>().GetSoul();
            player.GetComponent<PlayerStats>().SetSoul(currentSoul + 1);
        }

        // Interact with the pedestal
        if (gameObject.tag == "Pedestal")
        {
            if (player.GetComponent<PlayerStats>().GetSoul() > 0)
            {
                totalSouls = totalSouls - player.GetComponent<PlayerStats>().GetSoul();
                player.GetComponent<PlayerStats>().SetSoul(0); // clear all soul
                
                // Destroy pedestal
                if (totalSouls == 0)
                {
                    manager.GetComponent<EventManager>().setDestroy(gameObject);
                    player.GetComponent<PlayerStats>().SetSanity(100);
                }
            }
        }

        //Game ends
        if (player.GetComponent<PlayerStats>().GetSoul() == 4) {
            SceneManager.LoadScene("Cut");
        }

        if (totalSouls == 0)
        {
            SceneManager.LoadScene("Cut");
        }
    }
}


// Tutorial used: https://www.youtube.com/watch?v=AQc-NM2Up3M&list=PLfhbBaEcybmgidDH3RX_qzFM0mIxWJa21
