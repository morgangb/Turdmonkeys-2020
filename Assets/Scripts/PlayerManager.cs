﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public string KidType;

    PhotonView PV;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", KidType), new Vector3(0, 5f, 0), Quaternion.identity);
    }
}
