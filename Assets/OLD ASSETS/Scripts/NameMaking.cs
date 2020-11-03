using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameMaking : MonoBehaviour
{

    public InputField nameInput;
    public Button setNameBtn;


    

   
    public void OnTFChange(string value)
    {
        if (value.Length > 3)
        {
            setNameBtn.interactable = true;
        }
    }

    public void OnClick_SetName()
    {
        PhotonNetwork.NickName = nameInput.text;
    }

}
