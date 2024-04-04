using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class mineModeManager : MonoBehaviourPun
{
    public PlayerContainer playerContainer;
    public PlayerSpawner playerSpawner;
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
            if (allDead)
            {
                StartCoroutine(WaitForReload());
            }
            else
            {
                Debug.Log("아직 누가 살음");
                playerContainer.ReturnPlayerisDead(player1);
            }
        }
    }

    IEnumerator WaitForReload()
    {
        if (!isReloadingScene)
        {
            isReloadingScene = true;
            Debug.Log("전부 사망");
            yield return new WaitForSeconds(time);
            round++;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            isReloadingScene = false;
        }
    }

    public int RealMineReturn()
    {
        return round;
    }
}
