using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChair : Moment
{
    float time = 1;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-5f), ForceMode.Impulse);
    }

    void Update(){

    }
}
