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

    [SerializeField] float sanity;
    private float sanityDropRate;
    private bool sanityDropping;
    private GlitchEffect playerCamera;

    public void Start()
    {
        numOfSoul = 0;
        numOfKey = 0;
        sanity = 100;
        sanityDropRate = 1;
        sanityDropping = true;
        playerCamera = Camera.main.GetComponent<GlitchEffect>();
        StartSanityPlay();
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
        if(sanity < 50){
            playerCamera.intensity = (50 - sanity) * 0.02f; 
            playerCamera.colorIntensity = (50 - sanity) * 0.02f;
            playerCamera.flipIntensity  = (50 - sanity) * 0.001f; 
        }else{
            playerCamera.intensity = 0;
            playerCamera.colorIntensity = 0;
            playerCamera.flipIntensity = 0;
        }
    }

    public void StartSanityPlay(){
        StartCoroutine(DropSanityInTheDark());
    }
    
    public void SanityPlay(bool sanityPlay){
        sanityDropping = sanityPlay;
    }

    public void AffectSanity(float amount){
        sanity += amount;
    }

    public void ToggleSanityDrain(bool sanityDropping){
        this.sanityDropping = sanityDropping;
    }

    IEnumerator DropSanityInTheDark(){
        while(true){
            while(sanityDropping){
                yield return new WaitForSeconds(1);
                sanity -= sanityDropRate;
            }while(!sanityDropping){
                yield return new WaitForSeconds(1);
                print("Sanity not dropping");
            }
        }
    }
}
