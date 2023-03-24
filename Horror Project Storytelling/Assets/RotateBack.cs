using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBack : Moment
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
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
