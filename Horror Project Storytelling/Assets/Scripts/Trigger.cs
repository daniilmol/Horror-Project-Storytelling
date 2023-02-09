using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject moment;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            print("Player entered trigger");
            moment.SetActive(true);
        }
    }
}
