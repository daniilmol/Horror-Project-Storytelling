using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Vector3 pos;
    private bool canSeePlayer;

    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;
    private float timer;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        StartPatrol();
    }

    void Update()
    {
        UpdateAnimator();
        FireRayCasts();
        if(!canSeePlayer){
            Patrol();
        }else if(canSeePlayer){
            SearchPlayer();
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float forwardSpeed = localVelocity.z;
        animator.SetFloat("forwardSpeed", forwardSpeed);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    NavMeshPath path;
    bool CalculateNewPath() {
         agent.CalculatePath(GameObject.FindGameObjectWithTag("Player").transform.position, path);
         print("New path calculated");
         if (path.status != NavMeshPathStatus.PathComplete) {
             return false;
         }
         else {
             print("Path Valid");
             return true;
         }
     }

     private void StartPatrol(){
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
        pos = newPos;
        timer = 0;
     }

     private void Patrol(){
        timer += Time.deltaTime;
        if(timer >= wanderTimer || transform.position == pos){
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            pos = newPos;
            timer = 0;
        }
     }

     private void SearchPlayer(){
        agent.SetDestination(player.position);
     }

     private void FireRayCasts(){
        Vector3 targetPosition = player.position;
        Vector3 hostPosition = gameObject.transform.position + new Vector3(0, 1f, 0);
        Ray ray = new Ray(hostPosition, (targetPosition-hostPosition).normalized*10);
        Debug.DrawRay(hostPosition, (targetPosition-hostPosition).normalized*10);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100)){
            if(hit.transform == player){
                canSeePlayer = true;
                print("CAN SEE PLAYER");
            }else{
                canSeePlayer = false;
                print("CANNOT SEE PLAYER");
            }
        }
    }
}
