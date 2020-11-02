using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class KidController : MonoBehaviour
{
    [SerializeField] private GameObject myRobot;
    [SerializeField] private Camera myCamera;
    [SerializeField] private AudioListener myListener;
    
    private bool isRobot; //Is the player controlling the robot rn?
    private TurdmonkeysFirstPersonController myController;

    // Start is called before the first frame update
    void Start()
    {
        myController = GetComponent<TurdmonkeysFirstPersonController>(); //get the fps cont
    }

    // Update is called once per frame
    void Update()
    {
        //Toggle isrobot
        if (Input.GetButtonDown("Switch")) {
            isRobot = !isRobot;
        }

        //set active based on isrobot
        myRobot.SetActive(isRobot);
        myListener.enabled = !isRobot;
        myCamera.enabled = !isRobot;
        myController.enabled = !isRobot;
    }
}
