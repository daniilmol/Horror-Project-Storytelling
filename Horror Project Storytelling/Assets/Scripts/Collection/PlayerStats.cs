using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Soul stat")]
    public Text numOfSoulText;
    private int numOfSoul;

    [Header("Key stat")]
    public Text numOfKeyText;
    private int numOfKey;



    public void Start()
    {
        numOfSoul = 0;
        numOfKey = 0;
    }


    public void Update()
    {
        setText(numOfSoulText, "Soul: " + numOfSoul.ToString());
        setText(numOfKeyText, "Key: " + numOfKey.ToString());
    }
    private void setText(Text text, string numText)
    {
        text.text = numText;
    }

    public int GetSoul()
    {
        return this.numOfSoul;
    }

    public void SetSoul(int num)
    {
        Debug.Log(num);
        this.numOfSoul = num;
    }

    public int GetKey()
    {
        return this.numOfKey;
    }

    public void SetKey(int num)
    {
        this.numOfKey = num;
    }


}
