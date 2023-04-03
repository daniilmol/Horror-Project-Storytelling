using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moment : MonoBehaviour
{
    public static int numMoments = 0;
    public static bool runMannequin = false;
    [SerializeField] protected GameObject momentObject;
    [SerializeField] int required;

    public int GetRequired(){
        return required;
    }
}
