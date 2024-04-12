using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections.Generic;



public class mineModeManager : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public RoundData roundData;
    public PlayerSpawner playerSpawner;
    string playerId;

    public isDeadContainer container;

    public ClearUI clearUI;

    private int totalPlayers = 0;

    private int round;
    public float time = 3;
    bool allDead = false;
    bool isReloadingScene = false;

    public int aliveMan = 0;
    public int deadMan = 0;

    void Awake()
    {
        aliveMan = 0;
        deadMan = 0;
        roundData.AddClear(false);
        totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("���� ���� ���� �÷��̾� ��: " + totalPlayers);

        round = roundData.ReturnRound();
        Debug.Log("���� " + round + "���� ������");

        playerId = PhotonNetwork.LocalPlayer.UserId;
    }

    void Update()
    {
        CheckAlive();
        if (roundData.ReturnRound() == 5 && container.ReturnisAllDead() && Input.GetMouseButtonDown(0))
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel("Lobby");
        }

    }

    public void CheckAlive()
    {
        if (!isReloadingScene && playerContainer.ReturnPlayerisDeadAll())
        {
            isReloadingScene = true;
            StartCoroutine(ifAllDead());
        }
        else if (!isReloadingScene && (deadMan != totalPlayers) && (aliveMan > (totalPlayers / 2)) || aliveMan > 0 && (aliveMan + deadMan == totalPlayers))
        {
            isReloadingScene = true;
            StartCoroutine(ifClearLevel());
        }
    }

    IEnumerator ifAllDead()
    {
        if (roundData.ReturnRound() == 5)
        {
            roundData.AddClearGame(true);

        }
        Debug.Log("���� ���, ���� ���� �����");
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator ifClearLevel()
    {
        Debug.Log("���� ���� �ε�");
        yield return new WaitForSeconds(time);
        round++;
        roundData.AddClear(true);
        roundData.AddRound(round);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int RealMineReturn()
    {
        Debug.Log(round);
        return round;
    }
    public void AddLive(int aliveMan)
    {
        this.aliveMan = aliveMan;
    }

    public void AddDead(int deadMan)
    {
        this.deadMan = deadMan;
    }

}
