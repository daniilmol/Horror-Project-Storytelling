using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Vector3 pos;

    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;
    private float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        StartPatrol();
    }

    void Update()
    {
        Patrol();
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
        if(timer >= wanderTimer && transform.position == pos){
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            pos = newPos;
            timer = 0;
        }
     }
}
