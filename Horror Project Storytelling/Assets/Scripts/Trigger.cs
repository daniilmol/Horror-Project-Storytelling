using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    //Moments contain a single game object that's comprised of numerous game objects for the moment (audiosources, particles, ghosts)
    //Place a moment game object into this trigger for it to occur once the player touches it, always deactivate moments initially
    [SerializeField] GameObject moment;
    private MomentManager momentManager;

    void Start(){
        momentManager = GameObject.FindGameObjectWithTag("MomentManager").GetComponent<MomentManager>();
    }
    private void OnTriggerEnter(Collider other) {
        print("NUM MOEMNTS: " + Moment.numMoments);
        if(other.gameObject.tag == "Player" && Moment.numMoments >= moment.GetComponent<Moment>().GetRequired()){
            moment.GetComponent<Moment>().enabled = true;
            Moment.numMoments++;
            momentManager.MomentTriggered();
            Destroy(this.gameObject);
        }
    }
}
