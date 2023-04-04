using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightManager : MonoBehaviour
{
    private GameObject player;

    public Light[] hallLights;
    public GameObject[] hiddenLights;
    public Light[] storageRoomLights;
    public Light[] patientRoomLights;
    public Light[] facultyRoomLights;
    public Light[] cafeteriaLights;
    public int lightRange;

    public void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        setHallLights(lightRange);
       
    }

    public void setHallLights(int newRange) {
        for(int i = 0; i < hallLights.Length; i++) {
            hallLights[i].range = newRange;
        }
    }
    public void setStorageLights(int newRange) {
        for(int i = 0; i < storageRoomLights.Length; i++) {
            storageRoomLights[i].range = newRange;
        }
    }
    public void setFacultyLights(int newRange) {
        for(int i = 0; i < facultyRoomLights.Length; i++) {
            facultyRoomLights[i].range = newRange;
        }
    }
    public void setPatientLights(int newRange) {
        for(int i = 0; i < patientRoomLights.Length; i++) {
            patientRoomLights[i].range = newRange;
        }
    }

    public void unhideLight() {
        for( int i = 0; i < hiddenLights.Length; i++) {
            hiddenLights[i].SetActive(true);
        }
    }

    public void puzzleFinish() {
        lightRange = lightRange - 1;
        setHallLights(lightRange);
    }

    public void setLightColour(){
        for(int i = 0; i < hallLights.Length; i++) {
            hallLights[i].color = Color.red;
            hallLights[i].intensity = 1.3f;
        }
        for(int i = 0; i < storageRoomLights.Length; i++) {
            storageRoomLights[i].color = Color.red;
            storageRoomLights[i].intensity = 1.3f;
        }
        for(int i = 0; i < facultyRoomLights.Length; i++) {
            facultyRoomLights[i].color = Color.red;
            facultyRoomLights[i].intensity = 1.3f;
        }
        for(int i = 0; i < patientRoomLights.Length; i++) {
            patientRoomLights[i].color = Color.red;
            patientRoomLights[i].intensity = 1.3f;
        }
        for(int i = 0; i < cafeteriaLights.Length; i++) {
            cafeteriaLights[i].color = Color.red;
            cafeteriaLights[i].intensity = 1.3f;
        }
        player.GetComponent<PlayerStats>().SetSanity(60);
    }
}
