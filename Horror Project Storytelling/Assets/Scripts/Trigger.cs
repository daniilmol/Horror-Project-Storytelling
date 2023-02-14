using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //Moments contain a single game object that's comprised of numerous game objects for the moment (audiosources, particles, ghosts)
    //Place a moment game object into this trigger for it to occur once the player touches it, always deactivate moments initially
    [SerializeField] GameObject moment;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            print("Player entered trigger");
            moment.GetComponent<Moment>().enabled = true;
        }
    }
}
