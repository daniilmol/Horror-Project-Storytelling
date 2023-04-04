using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RunAtPlayer : Moment
{
    private Animator animator;
    private NavMeshAgent agent;
    private GameObject player;
    private PlayerStats playerStats;
    [SerializeField] GameObject momentToRun;
    [SerializeField] GameObject momentToRun2;
    [SerializeField] GameObject momentToHang;
    [SerializeField] GameObject[] skinnedMesh = {};

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        animator = momentObject.GetComponent<Animator>();
        agent = momentObject.GetComponent<NavMeshAgent>();
        playerStats.AffectSanity(-20f);
        animator.SetTrigger("runAt");
        Destroy(momentToRun);
        Destroy(momentToRun2);
        momentToHang.SetActive(true);
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
        ChasePlayer();
        print("DISTANCE: " + Vector3.Distance(player.transform.position, transform.position));
    }

    private void UpdateAnimator(){
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float forwardSpeed = localVelocity.z;
        animator.SetFloat("forwardSpeed", forwardSpeed);
    }

    private void ChasePlayer(){
        agent.SetDestination(player.transform.position);
    }   

    private void CheckDistance(){
        if(Vector3.Distance(player.transform.position, transform.position) < 1){
            print("OBJECT TOUCHED PLAYER");
            StopCoroutine(Disappear());
            FadeOut();
        }
    }

    IEnumerator Disappear(){
        yield return new WaitForSeconds(1.8f);
        FadeOut();
    }

    private void FadeOut(){
        print("MAKING OBJECT INVISIBLE");
        foreach(GameObject go in skinnedMesh){
            go.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        GameObject canvas = GameObject.Find("FadeIn");
        canvas.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        canvas.GetComponent<Animator>().SetTrigger("FadeIn");
        StartCoroutine(DestroyGameObject());
    }

    IEnumerator DestroyGameObject(){
        print("DESTROYING OBJECT");
        yield return new WaitForSeconds(2f);
        print("object destroyed");
        Destroy(momentObject);
    }
}
