using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakLight : Moment
{
    void Start(){
        momentObject.SetActive(false);
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        manager.GetComponent<AudioManager>().playLightBreak();
    }
}
