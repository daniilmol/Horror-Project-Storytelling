using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newsPopup : Interactable
{
    private GameObject player; // the gameObject attached with pause resume()..
    public GameObject[] imagePop; // Image to pop up
    public GameObject instructionText;

    private int pointer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pointer = 0;
    }
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Resume();
        // }
    }
    public override void OnInteract()
    {
        imagePop[pointer].SetActive(false);
        if(pointer < 9){ 
            pointer++;
        }
        else{
            pointer = 0;
        }
    }
    public void Resume()
    {
        // resume game
        Time.timeScale = 1f;
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        imagePop[pointer].SetActive(false);
        instructionText.SetActive(false);
    }

    public override void OnFocus()
    {
        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = true;
        Time.timeScale = 0f;
        imagePop[pointer].SetActive(true);
        instructionText.SetActive(true);
    }

    public override void OnLoseFocus()
    {
        Resume();
       
    }
}
