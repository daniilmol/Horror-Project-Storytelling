using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NewsPaperPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player; // the gameObject attached with pause resume()..

    public GameObject newsPaper;
    public GameObject enterBtn;

    public TextMeshProUGUI[] culprits;

    public TextMeshProUGUI[] victims;
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
    public void checkAnswers()
    {
        string[] culpList = new string[2];
        string[] victimList = new string[3];
        culpList = cleanArray(culprits);
        victimList = cleanArray(victims);
        bool richard = Array.Exists(culpList, element => element == "richard");
        bool rain = Array.Exists(culpList, element => element == "rain");
        bool mary = Array.Exists(victimList, element => element == "mary");
        bool annie = Array.Exists(victimList, element => element == "annie");
        bool charles = Array.Exists(victimList, element => element == "charles");
        if (richard && rain && mary && annie && charles)
        {
            enterBtn.SetActive(false);
            shard.SetActive(true);
            Resume();
            manager.GetComponent<AudioManager>().playRight();
            manager.GetComponent<LightManager>().puzzleFinish();
        }
        else
        {
            timesFailed++;
            manager.GetComponent<AudioManager>().playWrong();
            if (timesFailed >= 3)
            {
                manager.GetComponent<EventManager>().SetHint(hint);
            }
        }
    }

    public string[] cleanArray(TextMeshProUGUI[] initial)
    {
        string[] newList = new String[initial.Length];
        for (int i = 0; i < initial.Length; i++)
        {
            string result = "";
            for (int x = 0; x < initial[i].text.Length - 1; x++)
            {
                result += initial[i].text.Substring(x, 1);
            }
            newList[i] = result.ToLower().Trim();
        }
        return newList;
    }
}
