using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] GameObject spawnPointParent;
    [SerializeField] GameObject girl;
    private GameObject player;
    private PlayerStats playerStats;
    private bool girlSpawned;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        girlSpawned = false;
    }

    void Update()
    {
        if(playerStats.GetSanity() < 30 && !girlSpawned){
            SpawnGirl();
        }else if(playerStats.GetSanity() >= 30 && girlSpawned){
            DespawnGirl();
        }
    }

    public void SpawnGirl(){
        girlSpawned = true;
        Transform[] spawnPoints = new Transform[3];
        int i = 0;
        foreach(Transform child in spawnPointParent.transform){
            spawnPoints[i++] = child;
        }
        GameObject girl = Instantiate(Resources.Load("GhostGirl") as GameObject, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        print("GIRL HAS BEEN INSTANTIATED");
    }

    private void DespawnGirl(){
        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        girlSpawned = false;
    }
}
