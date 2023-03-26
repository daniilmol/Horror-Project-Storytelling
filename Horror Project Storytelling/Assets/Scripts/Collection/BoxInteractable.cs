using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractable : Interactable
{
    private GameObject player;
    private GameObject manager;

    public string message;



    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }


    public override void OnFocus()
    {
        // when you look at the gameobject, play sound or smt

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

        manager.GetComponent<EventManager>().SetScriptText(message);  // replace the text

    }

}


// Tutorial used: https://www.youtube.com/watch?v=AQc-NM2Up3M&list=PLfhbBaEcybmgidDH3RX_qzFM0mIxWJa21
