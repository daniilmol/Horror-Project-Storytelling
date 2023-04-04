using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private GameObject player;
    public Text scriptText;

    // Animation
    [Header("Animation")]
    public Animator pedestalAnimator;


    // check if need to destoy a object
    private bool destroyPedestal = false;
    private GameObject pedstal;

    public void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
       
    }
    public void Update()
    {
        if (destroyPedestal)
        {
            if(pedestalAnimator != null)
            {
                if (pedestalAnimator.GetCurrentAnimatorStateInfo(0).IsName("End"))
                {
                    Destroy(pedstal);
                    pedestalAnimator = null;
                }
            }
            

        }
    }
 
    public void setDestroy(GameObject obj)
    {
        if(obj.tag == "Pedestal")
        {
            destroyPedestal = true;
            pedstal = obj;
            if (pedestalAnimator != null)
            {
                pedestalAnimator.enabled = true;
            }
        }
    }

    public void SetScriptText(string s)
    {
        if(s != this.scriptText.text){
            scriptText.text = s;
            this.CallReset(3.0f);
        } 
    }
    public void SetHint(string s)
    {
        if(s != this.scriptText.text){
            scriptText.text = s;
            this.CallReset(4.5f);
        } 
    }

    public void SetDialogueText(string s, float time) {
        if(s != this.scriptText.text){
            scriptText.text = s;
            this.CallReset(time);
        } 
    }

    public void HoverScript(string s) {
        if(s != this.scriptText.text){
            scriptText.text = s;
            this.CallReset(1.0f);
        } 
    }

    private void ResetScriptText()
    {
        scriptText.text = "";
    }

    public void CallReset(float time)
    {
        Invoke("ResetScriptText", time);
    }

    

    
}
