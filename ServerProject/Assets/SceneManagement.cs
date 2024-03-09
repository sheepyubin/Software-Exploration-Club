using UnityEngine;
using Photon.Pun;
using System.Collections;
public class SceneManagement : MonoBehaviour
{
    public PlayerContainer container;
    public string nextSceneName;

    public GameObject scoreBoard;
    public ScoreBoard scoreBoardScrpit;
    public float delayTIme = 3f;
    // 씬 전환 함수
    public void Update()
    {
        bool allDead = container.CheckAllDead();

        // 모든 플레이어가 사망한 경우에만 씬을 전환
        if (allDead)
        {
            End();
        }
    }


    public void End()
    {
        scoreBoard.SetActive(true);
        scoreBoardScrpit.Score();

        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (nextSceneName == "Lobby")
            PhotonNetwork.Disconnect();

        PhotonNetwork.LoadLevel(nextSceneName);
    }
}
