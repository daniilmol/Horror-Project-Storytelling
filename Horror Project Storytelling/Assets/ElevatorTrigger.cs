using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private bool elevatorMove = false;
    private float riseSpeed = 3f;
    public GameObject leftOpenDoor;
    public GameObject rightOpenDoor;
    private float doorOpenSpeed = 0.8f;
    private float openTimer = 2f;
    private float closeTimer = 2f;
    private float riseTimer = 5f;
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
                    transform.position = transform.position + new Vector3(0, riseSpeed, 0) * Time.deltaTime;
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
        
        leftOpenDoor.transform.position = leftOpenDoor.transform.position - new Vector3(0, 0, doorOpenSpeed) * Time.deltaTime;
        rightOpenDoor.transform.position = rightOpenDoor.transform.position + new Vector3(0, 0, doorOpenSpeed) * Time.deltaTime;
    }

    private void elevatorDoorClose()
    {

        leftOpenDoor.transform.position = leftOpenDoor.transform.position + new Vector3(0, 0, doorOpenSpeed) * Time.deltaTime;
        rightOpenDoor.transform.position = rightOpenDoor.transform.position - new Vector3(0, 0, doorOpenSpeed) * Time.deltaTime;
    }
}
