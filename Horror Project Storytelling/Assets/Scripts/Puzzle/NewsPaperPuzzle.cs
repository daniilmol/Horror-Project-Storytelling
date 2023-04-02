using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewsPaperPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player; // the gameObject attached with pause resume()..

    public GameObject newsPaper;
    public Button enterButton;

    public TextMeshProUGUI culprit1;
    public TextMeshProUGUI culprit2;

    public TextMeshProUGUI victim1;
    public TextMeshProUGUI victim2;
    public TextMeshProUGUI victim3;
    public string hint;
    private int timesFailed = 0;
    private GameObject manager;

    public GameObject shard;
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
    public void Resume()
    {
        // resume game
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        newsPaper.SetActive(false);
    }

    public void Hi(){
        Debug.Log("hi");
    }

    public void EnterText()
    {
        string entered = victim1.text;
        string result = "";
        for (int i = 0; i < entered.Length - 1; i++)
        {
            result += entered.Substring(i, 1);
        }
        Debug.Log("hi");
        Debug.Log(culprit1);
        Debug.Log(culprit2);
        Debug.Log(victim1);
        Debug.Log(victim2);
        Debug.Log(victim3);
        // if (result == "8416")
        // {
        //     shard.SetActive(true);
        //     Resume();
        //     Destroy(gameObject);
        //     puzzlePage.text = newPageText;
        // }
        // else
        // {
        //     victim1.text = "";
        //     timesFailed++;
        //     if (timesFailed >= 3)
        //     {
        //         if (timesFailed == 3)
        //         {
        //             Resume();
        //         }
        //         manager.GetComponent<EventManager>().SetHint(hint);
        //     }
        // }
    }
}
