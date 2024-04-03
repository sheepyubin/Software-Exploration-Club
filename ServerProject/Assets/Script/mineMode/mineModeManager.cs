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

    bool allDead = false;
    public string player1;
    public string player2;
    public string player3;
    public string player4;

    bool isReloadingScene = false; 

    // Start is called before the first frame update
    void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.UserId;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAlive();
    }

    public void CheckAlive()
    {
        if (!isReloadingScene)
        {
            allDead = playerContainer.ReturnPlayerisDeadAll();
            if (allDead == true)
            {
                Debug.Log("전부 사망");
                playerData.Returnplayer();
                StartCoroutine(WaitForSecondsCoroutine(5f));
                round++;
                isReloadingScene = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Debug.Log("아직 누가 살음");
                playerContainer.ReturnPlayerisDead(player1);
            }
        }
    }

    public int RealMineReturn()
    {
        return round;
    }

    IEnumerator WaitForSecondsCoroutine(float waitTime)
    {
        Debug.Log(waitTime + " 초 기다림...");

        yield return new WaitForSeconds(waitTime);

        Debug.Log("끼얏호우!");
    }
}
