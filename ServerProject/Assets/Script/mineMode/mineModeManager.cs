using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


public class mineModeManager : MonoBehaviourPun
{
    public PlayerContainer playerContainer;
    public PlayerSpawner playerSpawner;
    public PlayerData playerData;
    string playerId;

    public string player1;
    public string player2;    
    public string player3;
    public string player4;

    // Start is called before the first frame update
    void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.UserId;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerContainer.ReturnPlayerisDead(playerId))
        {

        }
    }

    public void CheckAlive()
    {
        bool allDead = false;
        allDead = playerContainer.ReturnPlayerisDeadAll();
        if (allDead == true)
        {
            Debug.Log("���� ���");
            playerData.Returnplayer();
        }
        else
        {
            Debug.Log("���� ���� ����");
            playerContainer.ReturnPlayerisDead(player1);
        }
    }

}
