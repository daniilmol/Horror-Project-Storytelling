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

    private float sanity;
    private float sanityDropRate;
    private bool sanityDropping;

    public void Start()
    {
        numOfSoul = 0;
        numOfKey = 0;
        sanity = 100;
        sanityDropRate = 0;
        sanityDropping = false;
    }


    public void Update()
    {
        setText(numOfSoulText, "Soul: " + numOfSoul.ToString());
        setText(numOfKeyText, "Key: " + numOfKey.ToString());
        ChangePlayerViewBasedOnSanity();
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

    private void ChangePlayerViewBasedOnSanity(){
        switch(sanity){
            case 50:
            break;
            case 25:
            case 10:
            default:
            break;
        }
    }

    public void StartSanityPlay(){
        StartCoroutine(DropSanityInTheDark());
    }
    
    public void SanityPlay(bool sanityPlay){
        sanityDropping = sanityPlay;
    }

    public void DecreaseSanity(float amount){
        sanity -= amount;
    }

    public void IncreaseSanity(float amount){
        sanity += amount;
    }

    public void ToggleSanityDrain(bool sanityDropping){
        this.sanityDropping = sanityDropping;
    }

    IEnumerator DropSanityInTheDark(){
        while(sanityDropping){
            yield return new WaitForSeconds(1);
            sanity -= sanityDropRate;
        }
    }
}
