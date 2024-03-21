using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelect : MonoBehaviourPunCallbacks
{
    public GameObject[] Gamemodes;
    public GameObject modeSelecter;

    private string gameMode;

    private void Start()
    {
        gameMode = "Stage_1";

        modeSelecter.transform.position = Gamemodes[0].transform.position;
    }
    public void Baisc()
    {
        gameMode = "Stage_1";

        modeSelecter.transform.position = Gamemodes[0].transform.position;
    }

    public void Boom()
    {
        gameMode = "Boom";

        modeSelecter.transform.position = Gamemodes[1].transform.position;
    }

    public void Lava()
    {
        gameMode = "Lava";

        modeSelecter.transform.position = Gamemodes[2].transform.position;
    }

    public string ReturnGameMode()
    {
        return gameMode;
    }
}
