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
    private float sanityRecoverRate;
    private GlitchEffect playerCamera;

    private Vector3 startingPosition;

    public void Start()
    {
        startingPosition = transform.parent.position;
        numOfSoul = 0;
        numOfKey = 0;
        sanity = 200;
        sanityDropRate = 1.5f;
        sanityRecoverRate = 0.5f;
        sanityDropping = true;
        playerCamera = Camera.main.GetComponent<GlitchEffect>();
        StartSanityPlay();
    }


    public void Update()
    {
        setText(numOfSoulText, "Soul: " + numOfSoul.ToString());
        setText(numOfKeyText, "Key: " + numOfKey.ToString());
        ChangePlayerViewBasedOnSanity();
        PreventNegativeSanity();
    }

    private void PreventNegativeSanity(){
        if(sanity <= 0){
            sanityDropRate = 0;
        }else{
            sanityDropRate = 1.5f;
        }
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

    private void ChangePlayerViewBasedOnSanity(){
        if(sanity < 70){
            playerCamera.intensity = (70 - sanity) * 0.02f; 
            playerCamera.colorIntensity = (70 - sanity) * 0.02f;
            playerCamera.flipIntensity  = (70 - sanity) * 0.001f; 
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

    public float GetSanity(){
        return sanity;
    }

    public void SetSanity(float newSanity){
        sanity = newSanity;
    }

    public void ToggleSanityDrain(bool sanityDropping){
        this.sanityDropping = sanityDropping;
    }

    public Vector3 GetStartingPosition(){
        return startingPosition;
    }

    IEnumerator DropSanityInTheDark(){
        while(true){
            while(sanityDropping){
                yield return new WaitForSeconds(1);
                sanity -= sanityDropRate;
            }while(!sanityDropping){
                yield return new WaitForSeconds(1);
                sanity += sanityRecoverRate;
            }
        }
    }
}
