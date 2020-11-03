using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class SyncLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.Instantiate("Kid Controller", new Vector3(0, 1, 0), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
