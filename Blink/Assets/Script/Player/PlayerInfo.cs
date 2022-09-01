using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public int saveSpot;
    public PlayerInfo()
    {
    }
    public PlayerInfo(PlayerController2 player)
    {
        saveSpot = WorldController.Instance.hightestSpot;
    }
}
