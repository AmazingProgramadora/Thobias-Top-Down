using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPlayer : MonoBehaviour
{
    public static ManagerPlayer Instance;
    public PlayableCharacter[] playerCharacters = new PlayableCharacter[2];
    public int activePlayer;
    public int GetInactivePlayerIndex()
    {
        if (activePlayer.Equals(0))
        {
            return 1;
        }
        else return 0;
    }
    private void Start()
    {
        int legal = GetInactivePlayerIndex();
    }
    private void Awake()
    {
        Instance = this;
    }
}

