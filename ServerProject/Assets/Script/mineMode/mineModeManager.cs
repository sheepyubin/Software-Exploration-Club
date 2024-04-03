using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mineModeManager : MonoBehaviourPun
{
    public PlayerContainer playerContainer;
    public PlayerSpawner playerSpawner;
    public PlayerData playerData;
    string playerId;

    public int round = 1;

    public float time = 5;

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
            Debug.Log("전부 사망");
            playerData.Returnplayer();
            while(true)
            {
                if (time == 5)
                {
                    break;
                }
                time += Time.deltaTime;
            }
            round++;
            SceneManager.LoadScene("Boom");
            
        }
        else
        {
            Debug.Log("아직 누가 살음");
            playerContainer.ReturnPlayerisDead(player1);
        }
    }

    public int RealMineReturn()
    {
        return round;
    } 

}
