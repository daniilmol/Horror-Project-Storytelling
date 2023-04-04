using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBack : Moment
{
    private Animator animator;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerStats.AffectSanity(-10f);
        animator = momentObject.GetComponent<Animator>();
        animator.SetTrigger("Trigger");
        StartCoroutine(StartAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAnimation(){
        yield return new WaitForSeconds(0.25f);
        Destroy(momentObject);
    }
}
