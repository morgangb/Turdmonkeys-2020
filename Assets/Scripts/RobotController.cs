using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Photon.Pun;

public class RobotController : MonoBehaviour
{
    [SerializeField] private float dist = 10f;
    [SerializeField] private PostProcessVolume myPostProcessor;
    //[SerializeField] private GameObject gunEffect;
    [SerializeField] private int shootType;
    [SerializeField] private float baseDmg;
    [SerializeField] private float range;
    [SerializeField] private float flySpeed;
    
    private Grain grain = null;
    private KidController myKidController;
    private Camera myCamera;
    private int layerMask;
    
    public GameObject myKid;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PV.IsMine) { return; }

        myKidController = myKid.GetComponent<KidController>();
        myPostProcessor.profile.TryGetSettings(out grain);
        myCamera = GetComponentInChildren<Camera>();

        //layermask to avoid self in raycasting
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    void Update()
    {
       // gunEffect.SetActive(false);

        if(!PV.IsMine) { return; }

        //Find range from player
        float kidDist = Vector3.Distance(transform.position, myKid.transform.position);

        //Increase grain
        grain.size.value = 0f + 1.7f * (kidDist / dist);

        if (kidDist > dist) {
            myKidController.isRobot = false;
        }

        if ((shootType == 0 || shootType == 2) && Input.GetButton("Fire1")) { shoot(baseDmg * Time.deltaTime); }
        else if (Input.GetButtonDown("Fire1")) { shoot(baseDmg); }


        if (shootType == 2)
        {
            Debug.Log(Input.GetAxis("Fly") * Time.deltaTime * flySpeed);
            GetComponent<CharacterController>().Move(new Vector3(0, Input.GetAxisRaw("Fly") * Time.deltaTime * flySpeed, 0));

        }
    }

    private void shoot(float dmg)
    {
        print("Shooting");
        //gunEffect.SetActive(true);

        //Cast ray for robot stuff
        RaycastHit hit;
        if (Physics.Raycast(myCamera.transform.position, myCamera.transform.TransformDirection(Vector3.forward), out hit, range, layerMask))
        {
            if(hit.transform.gameObject.GetComponent<HitController>()) { hit.transform.gameObject.GetComponent<HitController>().takeHit(dmg, shootType); }
        }
    }
}
