using UnityEngine;
using Photon.Pun;

public class SyncPlayerContainer : MonoBehaviourPun
{
    public PlayerContainer playerContainer;

    // 포톤 RPC 메소드로써, 플레이어 색상을 동기화하는 함수
    [PunRPC]
    void SyncPlayerColor(string playerID, float r, float g, float b)
    {
        // RGB 값을 Color로 변환
        Color color = new Color(r, g, b);

        // 플레이어 색상 추가
        playerContainer.AddPlayerColor(playerID, color);
    }

    // 포톤 RPC 메소드로써, 플레이어 점수를 동기화하는 함수
    [PunRPC]
    void SyncPlayerScore(string playerID, int score)
    {
        playerContainer.AddPlayerScore(playerID, score);
    }

    // 포톤 RPC 메소드로써, 플레이어 생사 여부를 동기화하는 함수
    [PunRPC]
    void SyncPlayerIsDead(string playerID, bool isDead)
    {
        playerContainer.AddPlayerisDead(playerID, isDead);
    }
}
