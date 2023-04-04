using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatientBoardPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player; // the gameObject attached with pause resume()..
    // Dropdowns objects
    public TMP_Dropdown[] dropdowns;
    public GameObject enterBtn;
    // Hints
    public string hint;
    // Times a player has failed the puzzle
    private int timesFailed = 0;
    private int ansCorrect = 0;
    private GameObject manager;

    private ArrayList names;

    public GameObject shard;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("Manager");
        names = new ArrayList() { "Louis", "Annie", "Alice", "Charles", "Mary", "Adam" };
    }
    public void CheckAnswers()
    {
        for (int i = 0; i < names.Count; i++)
        {
            Debug.Log(dropdowns[i].options[dropdowns[i].value].text + " = " + names[i]);
            if (dropdowns[i].options[dropdowns[i].value].text == (string)names[i])
            {
                ansCorrect++;
            }
            else
            {
                break;
            }
        }
        if (ansCorrect == 6)
        {
            ansCorrect = 0;
            shard.SetActive(true);
            enterBtn.SetActive(false);
            manager.GetComponent<LightManager>().puzzleFinish();
            manager.GetComponent<AudioManager>().playRight();
        }
        else
        {
            ansCorrect = 0;
            timesFailed++;
            manager.GetComponent<AudioManager>().playWrong();
            if (timesFailed >= 3)
            {
                manager.GetComponent<EventManager>().SetHint(hint);
            }
        }
    }
}
