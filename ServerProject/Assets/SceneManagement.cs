using UnityEngine;
using Photon.Pun;

public class SceneManagement : MonoBehaviour
{
    public string sceneName;
    public PlayerContainer container;
    // 씬 전환 함수
    public void Update()
    {
        bool allDead = container.CheckAllDead();

        // 모든 플레이어가 사망한 경우에만 씬을 전환
        if (allDead)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
