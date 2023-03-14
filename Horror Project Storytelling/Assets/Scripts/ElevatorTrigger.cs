using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private bool elevatorMove = false;
    private float riseSpeed = 3f;
    public GameObject leftOpenDoor;
    public GameObject rightOpenDoor;
    private float doorOpenSpeed = 0.75f;
    private float openTimer = 2f;
    private float closeTimer = 2f;
    private float riseTimer = 5f;
    private Vector3 risePos = new Vector3(0, 3f, 0);
  //  private Vector3 leftPos = new Vector3(0, 3f, 0);
    // Start is called before the first frame update
    void Start()
    {
        //leftOpenDoor.transform.position = leftOpenDoor.transform.position + new Vector3(doorOpenSpeed, 0, 0) * Time.deltaTime;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (openTimer > 0)
        {
            openTimer -= Time.deltaTime;
            elevatorDoorOpen();
           // openTimer -= Time.deltaTime;
            Debug.Log(openTimer);
        }
            
        if (elevatorMove) {
            if (closeTimer > 0)
            {
                closeTimer -= Time.deltaTime;
                elevatorDoorClose();
                // openTimer -= Time.deltaTime;
                Debug.Log(closeTimer);
            }
            else {
                if (riseTimer > 0)
                {
                    riseTimer -= Time.deltaTime;
                   // transform.position = + new Vector3(0, riseSpeed, 0) * Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, risePos, Time.deltaTime * riseSpeed);
                }
                else {
                    elevatorMove = false;
                    openTimer = 2f;
                    closeTimer = 2f;
                    riseTimer = 5f;
                }
             
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Debug.Log("Entering");
            elevatorMove = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Exiting");
            transform.position = new Vector3(0, -0.75f, 0);
            elevatorMove = false;
        }
    }

    private void elevatorDoorOpen() {
        
        leftOpenDoor.transform.position = Vector3.Lerp(leftOpenDoor.transform.position, new Vector3(leftOpenDoor.transform.position.x, leftOpenDoor.transform.position.y, leftOpenDoor.transform.position.z - 1f), doorOpenSpeed * Time.deltaTime);
        rightOpenDoor.transform.position = Vector3.Lerp(rightOpenDoor.transform.position, new Vector3(rightOpenDoor.transform.position.x, rightOpenDoor.transform.position.y, rightOpenDoor.transform.position.z + 1f), doorOpenSpeed * Time.deltaTime); 

        //transform.position = Vector3.Lerp(transform.position, risePos, Time.deltaTime * riseSpeed);
    }

    private void elevatorDoorClose()
    {

        leftOpenDoor.transform.position = leftOpenDoor.transform.position + new Vector3(0, 0, doorOpenSpeed) * Time.deltaTime;
        rightOpenDoor.transform.position = rightOpenDoor.transform.position - new Vector3(0, 0, doorOpenSpeed) * Time.deltaTime;
    }
}
