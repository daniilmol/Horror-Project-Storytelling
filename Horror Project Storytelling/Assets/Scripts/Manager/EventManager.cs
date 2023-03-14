using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
   // private GameObject arrow;
    private GameObject player;
   // public ParticleSystem ps;
    public Text scriptText;

    [Range(2, 4)]
    public int totalNumOfSoul;

    public void Start()
    {
       // arrow = GameObject.FindGameObjectWithTag("Arrow");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        ActiveArrow();
    }

    private void ActiveArrow()
    {
        // if (player.GetComponent<PlayerStats>().GetSoul() >= totalNumOfSoul && arrow != null)
        // {
        //     // if current collected soul higher than required
        //     arrow.SetActive(true);
        // }
        // else
        // {
        //     arrow.SetActive(false);
        // }
    }

    public int GetTotalSoul()
    {
        // return total required soul-collect number
        return totalNumOfSoul;
    }

    public void PlayParticle()
    {
        // play soul family when interact with pedestal
      //  arrow.SetActive(false); 
        //ps.Play();
    }

    public void StopParticle()
    {
        //ps.Stop();
    }

    public void SetScriptText(string s)
    {
        if(s != this.scriptText.text){
            scriptText.text = s;
            this.CallReset(3.0f);
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
