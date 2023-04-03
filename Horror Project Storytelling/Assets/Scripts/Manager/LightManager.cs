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
    public void Update()
    {

    }

    public void setHallLights(int newRange) {
        for(int i = 0; i < hallLights.Length; i++) {
            hallLights[i].range = newRange;
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
        }
    }
}
