using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private GameObject NameInput;
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; } }
    [SerializeField]
    private string _nickName = "";
    
    public string NickName
    {
        get
        {
            int value = Random.Range(1000, 9999);
            return _nickName + value.ToString();
        }
    }
}
