using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class KidController : MonoBehaviour
{
    [SerializeField] private GameObject myRobot;
    [SerializeField] private Camera myCamera;
    [SerializeField] private AudioListener myListener;
    [SerializeField] private GameObject marker;
    [SerializeField] private float dist = 5f;
    [SerializeField] private float throwSpeed = 5f;
    
    private bool isRobot; //Is the player controlling the robot rn?
    private TurdmonkeysFirstPersonController myController;
    private GameObject myMarker;
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        //layermask to avoid self in raycasting
        layerMask = 1 << 8;
        layerMask = ~layerMask;

        myController = GetComponent<TurdmonkeysFirstPersonController>(); //get the fps cont
    }

    // Update is called once per frame
    void Update()
    {
        if (isRobot) {

        }
        else {
            //Cast ray for marker placement
            RaycastHit hit;
            Vector3 markerLoc;
            if (Physics.Raycast(myCamera.transform.position, myCamera.transform.TransformDirection(Vector3.forward), out hit, dist, layerMask)) {
                markerLoc = hit.point;
            }
            else {
                markerLoc = myCamera.transform.position + dist * myCamera.transform.TransformDirection(Vector3.forward);
            }

            //Make Marker
            if (Input.GetButtonDown("Switch")) {
                myMarker = Instantiate(marker, markerLoc, Quaternion.identity);
            }
            //Move Marker
            else if (Input.GetButton("Switch")) {
                myMarker.transform.position = markerLoc;
            }
        }

        
        //Toggle isrobot
        if (Input.GetButtonUp("Switch")) {
            isRobot = !isRobot;
            if (myMarker) { 
                myRobot.transform.position = myMarker.transform.position;
                myRobot.transform.rotation = transform.rotation;
                Destroy(myMarker);
            }
        }

        //set active based on isrobot
        myRobot.SetActive(isRobot);
        myListener.enabled = !isRobot;
        myCamera.enabled = !isRobot;
        myController.enabled = !isRobot;
    }
}
