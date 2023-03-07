using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public bool keyNeeded = false;              //Is key needed for the door
    public bool gotKey;                  //Has the player acquired key
    public GameObject keyGameObject;            //If player has Key,  assign it here
    public GameObject txtToDisplay;             //Display the information about how to close/open the door
    public GameObject zombie;
    public GameObject zombie2;
    public GameObject zombie3;

    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him


    enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    DoorState doorState = new DoorState();      //To check the current state of the door

    /// <summary>
    /// Initial State of every variables
    /// </summary>
    private void Start()
    {
        gotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        //txtToDisplay.SetActive(false);
        zombie.SetActive(false);
        zombie2.SetActive(false);
        zombie3.SetActive(false);

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        if (keyNeeded && keyGameObject == null)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Assign Key GameObject");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //txtToDisplay.SetActive(true);
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
        //txtToDisplay.SetActive(false);
    }

    private void Update()
    {
        setGotKey();
        //To Check if the player is in the zone
        if (playerInZone)
        {
            if (doorState == DoorState.Opened)
            {
                //txtToDisplay.GetComponent<Text>().text = "Press 'E' to Close";
                GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManager>().SetScriptText("Press 'E' to Close");
                doorCollider.enabled = false;
            }
            else if (doorState == DoorState.Closed || gotKey)
            {
               //txtToDisplay.GetComponent<Text>().text = "Press 'E' to Open";
               GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManager>().SetScriptText("Press 'E' to Open");
                doorCollider.enabled = true;
            }
            else if (doorState == DoorState.Jammed)
            {
                //txtToDisplay.GetComponent<Text>().text = "Needs Key";
                GameObject.FindGameObjectWithTag("Manager").GetComponent<EventManager>().SetScriptText("Needs Key");
                doorCollider.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && playerInZone)
        {
            doorOpened = !doorOpened;           //The toggle function of door to open/close

            if (doorState == DoorState.Closed && !doorAnim.isPlaying)
            {
                if (!keyNeeded)
                {
                    doorAnim.Play("Door_Open");
                    doorState = DoorState.Opened;
                    
                }
                else if (keyNeeded && !gotKey)
                {
                    if (doorAnim.GetClip("Door_Jam") != null)
                        doorAnim.Play("Door_Jam");
                    doorState = DoorState.Jammed;
                }
            }

            if (doorState == DoorState.Closed && gotKey && !doorAnim.isPlaying)
            {
                // door open
                int currentKey = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().GetKey();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().SetKey(currentKey - 1);
                Debug.Log(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().GetKey());
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;

                // trigger zombie
                zombie.gameObject.SetActive(true);
                zombie2.gameObject.SetActive(true);
                zombie3.gameObject.SetActive(true);
            }

            if (doorState == DoorState.Opened && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Close");
                doorState = DoorState.Closed;
                zombie.gameObject.SetActive(false);
                zombie2.gameObject.SetActive(false);
                zombie3.gameObject.SetActive(false);
            }

            if (doorState == DoorState.Jammed && !gotKey)
            {
                if (doorAnim.GetClip("Door_Jam") != null)
                    doorAnim.Play("Door_Jam");
                doorState = DoorState.Jammed;
            }
            else if (doorState == DoorState.Jammed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }
        }
    }

    private void setGotKey(){
        if( GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().GetKey() > 0){
            gotKey = true;
        }
        else{
            gotKey = false;
        }
    }
}
