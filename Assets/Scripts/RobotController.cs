using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RobotController : MonoBehaviour
{
    [SerializeField] private float dist = 10f;
    [SerializeField] private PostProcessVolume myPostProcessor;
    private Grain grain = null;
    public GameObject myKid;

    // Start is called before the first frame update
    void Start()
    {
        myPostProcessor.profile.TryGetSettings(out grain);
    }

    // Update is called once per frame
    void Update()
    {
        //Range from player
        grain.size.value = 0.3f + 2.7f * (Vector3.Distance(transform.position, myKid.transform.position) / dist);
    }
}
