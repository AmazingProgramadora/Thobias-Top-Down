using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlayer : MonoBehaviour
{
    #region Declarations
    public List<GameObject> keyList = new List<GameObject>();

    public static ManagerPlayer Instance;
    public PlayableCharacter[] playerCharacters = new PlayableCharacter[2]; //array
    public int activePlayer;

    #endregion
    //public GameObject PlayerFuture, PlayerPast;
    public int GetInactivePlayerIndex()
    {
        if (activePlayer.Equals(0))
        {
            return 1;
        }
        else return 0;
    }
    private void Awake()
    {
        Instance = this;
    }

}