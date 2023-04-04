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
    private GhostController ghostController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        SpawnGirl();
    }

    void Update()
    {
        if(playerStats.GetSanity() < 30 && !girlSpawned){
            ActivateGirl();
        }else if(playerStats.GetSanity() >= 30 && girlSpawned){
            DeactivateGirl();
        }
    }

    public void SpawnGirl(){
        Transform[] spawnPoints = new Transform[3];
        int i = 0;
        foreach(Transform child in spawnPointParent.transform){
            spawnPoints[i++] = child;
        }
        GameObject girl = Instantiate(Resources.Load("GhostGirl") as GameObject, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        ghostController = girl.GetComponent<GhostController>();
    }

    private void ActivateGirl(){
        girlSpawned = true;
        ghostController.ToggleActive();
    }

    private void DeactivateGirl(){
        girlSpawned = false;
        ghostController.ToggleActive();
    }
}
