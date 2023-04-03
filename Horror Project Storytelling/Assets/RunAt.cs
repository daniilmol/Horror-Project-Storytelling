using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAt : Moment
{
    [SerializeField] GameObject[] skinnedMesh = {};
    [SerializeField] GameObject momentToRun;
    [SerializeField] GameObject momentToRun2;
    private bool momentStarted = false;
    void Start(){
        foreach(GameObject go in skinnedMesh){
            go.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        GetComponent<BoxCollider>().enabled = true;
        momentToRun.SetActive(true);
        momentToRun2.SetActive(true);
    }
}
