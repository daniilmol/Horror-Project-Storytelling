using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakLight : Moment
{
    void Start(){
        Destroy(momentObject);
    }
}
