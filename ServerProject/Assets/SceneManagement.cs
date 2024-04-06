using UnityEngine;
using Photon.Pun;
using System.Collections;
public class SceneManagement : MonoBehaviour
{
    public string nextSceneName;

    public GameObject scoreBoard;
    public ScoreBoard scoreBoardScrpit;
    public float delayTIme = 3f;

    // �� ��ȯ �Լ�
    public void Update()
    {

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
