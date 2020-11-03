using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Photon.Pun;

public class RobotController : MonoBehaviour
{
    [SerializeField] private float dist = 10f;
    [SerializeField] private PostProcessVolume myPostProcessor;
    
    private Grain grain = null;
    private KidController myKidController;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        if(!PV.IsMine) { return; }

        //Find range from player
        float kidDist = Vector3.Distance(transform.position, myKid.transform.position);

        //Increase grain
        grain.size.value = 0.3f + 2.7f * (kidDist / dist);

        if (kidDist > dist) {
            myKidController.isRobot = false;
        }
    }
}
