using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelect : MonoBehaviourPunCallbacks
{
    string gameMode;
    public void Baisc()
    {
        gameMode = "Stage_1";
    }

    public void Boom()
    {
        gameMode = "Boom";
    }

    public void Lava()
    {
        gameMode = "Lava";
    }

    public string ReturnGameMode()
    {
        return gameMode;
    }
}
