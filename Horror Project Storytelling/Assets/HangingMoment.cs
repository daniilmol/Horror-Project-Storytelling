using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingMoment : Moment
{
    [SerializeField] Light[] patientRoomLights;
    private bool flashed;
    private PlayerStats playerStats;
    void Start(){
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerStats.AffectSanity(-35f);
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(true);
        }
        flashed = false;
        StartCoroutine(FlashLights());
        for(int i = 0; i < patientRoomLights.Length; i++){
            patientRoomLights[i].color = new Color32(255, 0, 0, 255);
        }
        StartCoroutine(StopMoment());
    }

    IEnumerator FlashLights(){
        while(true){
            float randomFlashTime = Random.Range(0.05f, 0.23f);
            yield return new WaitForSeconds(randomFlashTime);
            if(!flashed){
                for(int i = 0; i < patientRoomLights.Length; i++){
                    patientRoomLights[i].intensity = 0;
                    flashed = true;
                }
            }else if(flashed){
                for(int i = 0; i < patientRoomLights.Length; i++){
                    patientRoomLights[i].intensity = 1;
                    flashed = false;
                }
            }
        }
    }

    IEnumerator StopMoment(){
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < patientRoomLights.Length; i++){
            patientRoomLights[i].color = new Color32(255, 255, 255, 255);
            patientRoomLights[i].intensity = 1;
        }
        Destroy(this.gameObject);
    }
}
