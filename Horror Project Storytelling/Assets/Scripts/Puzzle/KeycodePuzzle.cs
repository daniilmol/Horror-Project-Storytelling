using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeycodePuzzle : Interactable
{
    // Start is called before the first frame update
    private GameObject player; // the gameObject attached with pause resume()..
    public GameObject keyCodePop; // Image to pop up
    public Button enterButton;
    public TextMeshProUGUI enteredText;
    public string hint;
    private int timesFailed = 0;
    private GameObject manager;

    public GameObject shard;

    [TextArea]
    public string newPageText;
    public TextMeshProUGUI puzzlePage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");

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

        // Interact with the soul shards
        if (gameObject.tag == "KeyCode")
        {
            // pause game
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
            keyCodePop.SetActive(true);
            Debug.Log("Active");
        }
    }
    public void Resume()
    {
        // resume game
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        keyCodePop.SetActive(false);
    }

    public void EnterText()
    {
        string entered = enteredText.text;
        string result = "";
        for (int i = 0; i < entered.Length - 1; i++)
        {
            result += entered.Substring(i, 1);
        }

        if (result == "8416")
        {
            shard.SetActive(true);
            Resume();
            Destroy(gameObject);
            puzzlePage.text = newPageText;
        }
        else
        {
            enteredText.text = "";
            timesFailed++;
            if (timesFailed >= 3)
            {
                if (timesFailed == 3)
                {
                    Resume();
                }
                manager.GetComponent<EventManager>().SetHint(hint);
            }
        }
    }

    public override void OnFocus()
    {
    }

    public override void OnLoseFocus()
    {
    }
}
