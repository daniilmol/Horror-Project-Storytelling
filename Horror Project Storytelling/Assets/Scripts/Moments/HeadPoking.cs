using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPoking : Moment
{
    [SerializeField] GameObject[] skinnedMesh = {};
    [SerializeField] GameObject momentToDisappear;
    private bool momentStarted = false;
    void Start(){
        foreach(GameObject go in skinnedMesh){
            go.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        momentToDisappear.SetActive(true);
    }
}
