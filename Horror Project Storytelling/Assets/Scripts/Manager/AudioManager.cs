using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource pageOpen;
    public AudioSource shardGet;
    public AudioSource ambient;
    public AudioSource wrong;
    public AudioSource right;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openPage(){
        pageOpen.Play();
    }
    public void getShard(){
        shardGet.Play();
    }
    public void startAmbient(){
        ambient.Play();
    }
    public void playWrong(){
        wrong.Play();
    }
    public void playRight(){
        right.Play();
    }
}
