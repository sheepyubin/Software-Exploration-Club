using UnityEngine;
using Photon.Pun;

public class SceneManagement : MonoBehaviour
{
    public string sceneName;
    // 씬 전환 함수
    public void Update()
    {
        // 씬에 있는 모든 플레이어를 가져옴
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        bool allDead = true;
        foreach (GameObject player in players)
        {
            // 각 플레이어의 상태를 확인하여 isDead가 false인 경우가 하나라도 있으면 전환하지 않음
            if (!player.GetComponent<Movement>().isDead)
            {
                allDead = false;
                break;
            }
        }

        // 모든 플레이어가 사망한 경우에만 씬을 전환
        if (allDead)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
