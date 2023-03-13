using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] GameObject spawnPointParent;
    [SerializeField] GameObject girl;

    void Start()
    {
        SpawnGirl();
    }

    void Update()
    {
        
    }

    public void SpawnGirl(){
        Transform[] spawnPoints = new Transform[3];
        int i = 0;
        foreach(Transform child in spawnPointParent.transform){
            spawnPoints[i++] = child;
        }
        girl = Instantiate(girl, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        print("GIRL HAS BEEN INSTANTIATED");
    }
}
