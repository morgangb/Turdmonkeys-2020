using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public Player Player { get; private set; }
    public bool Ready = false;

    public void SetPlayerInfo(Player player)
    {
        Player = player;    
        
        _text.text = player.NickName;
        //player.NickName = (string)player.CustomProperties["Name"];
        
    }
    
}
