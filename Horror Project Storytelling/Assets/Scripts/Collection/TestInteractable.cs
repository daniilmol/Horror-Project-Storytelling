using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : Interactable
{
    //private AudioController aCtrl;
    private GameObject player;
    private GameObject manager;
   // private IEnumerator coroutine;



    public void Start()
    {
       // aCtrl = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");

    }

    
    public override void OnFocus()
    {
        // when you look at the gameobject, play sound or smt
       
        if(gameObject.tag == "Key" || gameObject.tag == "Soul" || gameObject.tag == "Pedestal")
        {
             //Debug.Log("Look at " + gameObject.name);
            manager.GetComponent<EventManager>().SetScriptText("Press E to collect " + gameObject.name);
        }

        if(gameObject.tag == "Pedestal")
        {
            
        }
       
    }

    public override void OnLoseFocus()
    {
        // Debug.Log("Leave " + gameObject.name);
    }

    public override void OnInteract()
    {
        // press E
        Debug.Log("Interact " + gameObject.name);

        // Interact with the soul shards
        if (gameObject.tag == "Soul")
        {
            // destroy obj
            Destroy(gameObject);

            // play sound - soul collected sound
           // aCtrl.PlaySound(aCtrl.GetSound("soulCollecting")); // testing only
            //PlaySoulScript();
            manager.GetComponent<EventManager>().SetScriptText("something here...");  // replace the text
            //manager.GetComponent<EventManager>().CallReset(2.0f);
           
            
          

            // Add shards collection number
            int currentSoul = player.GetComponent<PlayerStats>().GetSoul();
            player.GetComponent<PlayerStats>().SetSoul(currentSoul+1);
        }


        // Interact with the key
        if (gameObject.tag == "Key")
        {
            // Destroy key
            Destroy(gameObject);

            // play sound - get key sound

            // Add keys collection number
            int currentKey = player.GetComponent<PlayerStats>().GetKey();
            player.GetComponent<PlayerStats>().SetKey(currentKey+1);
        }

        // Interact with the pedestal
        if(gameObject.tag == "Pedestal")
        {
            if (CheckSoulNumber())
            {
                player.GetComponent<PlayerStats>().SetSoul(0); // clear all soul

                // play sound - family show up
               // aCtrl.PlaySound(aCtrl.GetSound("soulCollecting")); // for testing only
                // soul of the families appear here
                //ameObject.FindGameObjectWithTag("Manager").GetComponent<EventManager>().PlayParticle();


                // Destroy pedestal
                Destroy(gameObject);
            }
        }

        // // Interact with the door
        // if(gameObject.tag == "Door")
        // {
        //     Debug.Log("interact door");
        //     // check if player has key(s)
        //     if(player.GetComponent<PlayerStats>().GetKey() >= 1)
        //     {
        //         int currentKey = player.GetComponent<PlayerStats>().GetKey();
        //         player.GetComponent<PlayerStats>().SetKey(currentKey - 1); // consume one key

        //         // play sound - open door

        //         // destroy door
        //         Destroy(gameObject);
        //     }
        // }


    }

    // Make sure player collect all soul
    private bool CheckSoulNumber()
    {
        //Debug.Log("current: " + player.GetComponent<PlayerStats>().GetSoul() + "\n total: " + totalNumOfSoul);
        int totalNumOfSoul = manager.GetComponent<EventManager>().GetTotalSoul();
        
        if (player.GetComponent<PlayerStats>().GetSoul() < totalNumOfSoul)
        {
            return false;
        }
        return true;
    }

   

    

   


}


// Tutorial used: https://www.youtube.com/watch?v=AQc-NM2Up3M&list=PLfhbBaEcybmgidDH3RX_qzFM0mIxWJa21
