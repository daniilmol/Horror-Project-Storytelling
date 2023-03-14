using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePopup : Interactable
{
    private GameObject player; // the gameObject attached with pause resume()..
    public GameObject imagePop; // Image to pop up
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
    public override void OnInteract()
    {
        // press E
        Debug.Log("Interact " + gameObject.name);

        // Interact with the soul shards
        if (gameObject.tag == "PuzzlePaper")
        {
            // pause game
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
            imagePop.SetActive(true);
            Debug.Log("Active");
        }
    }
    public void Resume()
    {
        // resume game
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        imagePop.SetActive(false);
    }

    public override void OnFocus()
    {
    }

    public override void OnLoseFocus()
    {
    }
}
