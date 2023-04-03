using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FinalTimelinePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player; // the gameObject attached with pause resume()..

    public GameObject page;
    public TextMeshProUGUI[] userAnswers;
    private string[] answers;
    public string hint;
    private int timesFailed = 0;
    private GameObject manager;

    public GameObject shard;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
        answers = new string[] { "ash", "charles", "annie", "richard", "rain" };
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
        page.SetActive(false);
    }
    public void checkAnswers()
    {
        int numCorrect = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            if (cleanString(userAnswers[i]) == answers[i])
            {
                numCorrect++;
            }
            else
            {
                Debug.Log(cleanString(userAnswers[i]).Length + " - " + answers[i].Length);
            }
        }
        if (numCorrect >= 5)
        {
            shard.SetActive(true);
            Resume();
            manager.GetComponent<LightManager>().puzzleFinish();
            manager.GetComponent<LightManager>().setLightColour();
        }
        else
        {
            timesFailed++;
            if (timesFailed >= 3)
            {
                manager.GetComponent<EventManager>().SetHint(hint);
            }
        }
    }

    public string cleanString(TextMeshProUGUI initial)
    {
        string result = "";
        for (int i = 0; i < initial.text.Length - 1; i++)
        {
            result += initial.text.Substring(i, 1);
        }
        return result.ToLower().Trim();
    }
}
