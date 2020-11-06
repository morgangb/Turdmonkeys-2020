using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    private string KidType;
    
    
    void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable() 
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 2)
        {
            GameObject myplayermanager = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            myplayermanager.GetComponent<PlayerManager>().KidType = KidType;
        }
    }

    public void SetKidType(string setto) 
    {
        KidType = setto;
        
    }
    public void ButtonDisable(GameObject ToDisable)
    {
        ToDisable.GetComponent<Button>().interactable = false;
    }
}
