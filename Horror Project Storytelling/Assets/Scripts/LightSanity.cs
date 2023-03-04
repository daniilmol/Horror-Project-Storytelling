using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSanity : MonoBehaviour
{
    private Light lightObject;
    private SphereCollider sphereCollider;
    private PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        lightObject = GetComponent<Light>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = lightObject.range;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            player.ToggleSanityDrain(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            player.ToggleSanityDrain(true);
        }    
    }
}
