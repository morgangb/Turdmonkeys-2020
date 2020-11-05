using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.UI;

public class KidController : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private AudioListener myListener;
    [SerializeField] private GameObject marker;
    [SerializeField] private float dist = 5f;
    [SerializeField] private GameObject robot;
    [SerializeField]
    private string RobotType;
    
    private GameObject myRobot;
    private TurdmonkeysFirstPersonController myController;
    private TurdmonkeysFirstPersonController myRobotController;
    private Camera myRobotCamera;
    private AudioListener myRobotListener;
    private GameObject myMarker;
    private int layerMask;
    private GameObject grabbing;
    private Canvas myRoboCanvas;

    public bool isRobot; //Is the player controlling the robot rn?
    public bool hasRobot; //Does the player have the robot rn?

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PV.IsMine) { return; }

        //layermask to avoid self in raycasting
        layerMask = 1 << 8;
        layerMask = ~layerMask;
        
        myRobot = PhotonNetwork.Instantiate(Path.Combine("Prefabs", RobotType), transform.position, Quaternion.identity);
        myRobot.GetComponent<RobotController>().myKid = gameObject;
        myRobot.SetActive(false);        
        myRobotController = myRobot.GetComponent<TurdmonkeysFirstPersonController>();
        myRobotCamera = myRobot.GetComponentInChildren<Camera>();
        myRobotListener = myRobot.GetComponentInChildren<AudioListener>();
        myRoboCanvas = myRobot.GetComponentInChildren<Canvas>();
        myController = GetComponent<TurdmonkeysFirstPersonController>(); //get the fps cont
    }

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine) { return; }

        if (!isRobot)
        {
            //Cast ray for robot stuff
            RaycastHit hit;
            Vector3 markerLoc;
            if (Physics.Raycast(myCamera.transform.position, myCamera.transform.TransformDirection(Vector3.forward), out hit, dist))
            {
                markerLoc = hit.point;
            }
            else
            {
                markerLoc = myCamera.transform.position + dist * myCamera.transform.TransformDirection(Vector3.forward);
            }

            if (hasRobot)
            {
                //Make Marker
                if (Input.GetButtonDown("Switch"))
                {
                    myMarker = Instantiate(marker, markerLoc, Quaternion.identity);
                }
                //Move Marker
                else if (Input.GetButton("Switch"))
                {
                    myMarker.transform.position = markerLoc;
                }
            }

            //Grabbin
            if (Input.GetButtonDown("Grab") && hit.transform)
            {
                if (hit.transform.gameObject == myRobot)
                {
                    hasRobot = true;
                    myRobot.SetActive(false);
                }
                else if (hit.transform.GetComponent<GrabController>())
                {
                    grabbing = hit.transform.gameObject;
                    grabbing.transform.rotation = transform.rotation;
                    grabbing.GetComponent<GrabController>().grabber = gameObject;
                    transform.SetParent(grabbing.transform);
                    myController.enabled = false;
                }
            } //Let-goin
            else if (Input.GetButtonUp("Grab"))
            {
                if (grabbing)
                {
                    myController.enabled = true;
                    grabbing.GetComponent<GrabController>().grabber = null;
                    transform.SetParent(null);
                    grabbing = null;
                }
            }
        }

        //Toggle isrobot
        if (Input.GetButtonUp("Switch"))
        {
            isRobot = !isRobot;
            hasRobot = false;

            //set active based on isrobot
            myRobotListener.enabled = isRobot;
            myRobotCamera.enabled = isRobot;
            myRobotController.enabled = isRobot;
            myListener.enabled = !isRobot;
            myCamera.enabled = !isRobot;
            myController.enabled = !isRobot;
            myRoboCanvas.enabled = isRobot;

            if (myMarker)
            {
                myRobot.transform.position = myMarker.transform.position;
                myRobot.transform.rotation = transform.rotation;
                Destroy(myMarker);
            }
            myRobot.SetActive(true);
        }
        
    }
}
