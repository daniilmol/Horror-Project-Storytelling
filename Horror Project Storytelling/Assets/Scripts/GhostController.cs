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
    private bool active;
    private bool visible;

    private GhostInteractable[] objects;
    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;
    [SerializeField] GameObject[] skinnedMesh = {};
    private float timer;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        active = false;
        visible = true;
        objects = GameObject.FindObjectsOfType<GhostInteractable>();
        StartPatrol();
        StartCoroutine(TossingObjects());
    }

    void Update()
    {
        UpdateAnimator();
        FireRayCasts();
        ActiveBehaviour();
        if(!canSeePlayer || !active){
            Patrol();
        }else if(canSeePlayer && active){
            SearchPlayer();
            KillPlayer();
        }
    }

    private void ActiveBehaviour(){
        if(active && !visible){
            visible = true;
            foreach(GameObject go in skinnedMesh){
                go.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }else if(!active && visible){
            visible = false;
            foreach(GameObject go in skinnedMesh){
                go.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
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

    private void CheckForObjects(){
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        for(int i = 0; i < objects.Length; i++){
            GameObject t = objects[i].gameObject;
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        if(minDist < 5){
            float[] forces = new float[3];
            for(int j = 0; j < forces.Length; j++){
                forces[j] = Random.Range(4, 10);
            }
            tMin.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(forces[0], forces[1], forces[2]), ForceMode.Impulse);
        }
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

     private void KillPlayer(){
        if(Vector3.Distance(player.transform.position, transform.position) < 1.3f){
            player.position = new Vector3(0, 1, 0);
        }
     }

     public void ToggleActive(){
        active = !active;
     }

     IEnumerator TossingObjects(){
        while(true){
            yield return new WaitForSeconds(10);
            print("CHECKING FOR OBJECTS");
            CheckForObjects();
        }
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
