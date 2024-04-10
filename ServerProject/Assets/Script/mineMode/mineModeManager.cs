using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;
using UnityEngine;



public class mineModeManager : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public RoundData roundData;
    public PlayerSpawner playerSpawner;
    string playerId;

    private int round;
    public float time = 5;
    bool allDead = false;
    public string player1;
    public string player2;
    public string player3;
    public string player4;
    bool isReloadingScene = false;

    // Start is called before the first frame update
    void Awake()
    {

        round = roundData.ReturnRound();
        Debug.Log("���� " + round + "���� ������");

        playerId = PhotonNetwork.LocalPlayer.UserId;
    }

    // Update is called once per frame
    void Update()
    {
        if (roundData.ReturnRound() == 5 && Input.GetMouseButtonDown(0))
        {
            PhotonNetwork.LoadLevel("Lobby");
        }
        CheckAlive();

    }

    public void CheckAlive()
    {
        if (!isReloadingScene && playerContainer.ReturnPlayerisDeadAll())
        {
            isReloadingScene = true;
            StartCoroutine(ifAllDead());
        }
        else if (!isReloadingScene && (roundData.ReturnDead() + roundData.ReturnLive() == 0))
        {
            isReloadingScene = true;
            StartCoroutine(ifClearLevel());
        }
    }

    IEnumerator ifAllDead()
    {
        if (roundData.ReturnRound() == 5)
        {
            yield return new WaitForSeconds(200);
            PhotonNetwork.LoadLevel("Lobby");
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
        roundData.AddRound(round);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int RealMineReturn()
    {
        Debug.Log(round);
        return round;
    }

}
