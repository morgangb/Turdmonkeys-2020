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
    
    private bool isRobot; //Is the player controlling the robot rn?
    private TurdmonkeysFirstPersonController myController;
    private GameObject myMarker;

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<TurdmonkeysFirstPersonController>(); //get the fps cont
    }

    // Update is called once per frame
    void Update()
    {
        if (isRobot) {

        }
        else {
            //Make Marker
            if (Input.GetButtonDown("Switch")) {
                RaycastHit hit;
                Ray ray = new Ray (myCamera.transform.position, transform.forward);
                Physics.Raycast(ray, out hit, 5f);
                myMarker = Instantiate(marker, hit.point);
            }
            //Marker
            else if (Input.GetButton("Switch")) {

            }
        }

        
        //Toggle isrobot
        if (Input.GetButtonUp("Switch")) {
            isRobot = !isRobot;
        }

        //set active based on isrobot
        myRobot.SetActive(isRobot);
        myListener.enabled = !isRobot;
        myCamera.enabled = !isRobot;
        myController.enabled = !isRobot;
    }
}
