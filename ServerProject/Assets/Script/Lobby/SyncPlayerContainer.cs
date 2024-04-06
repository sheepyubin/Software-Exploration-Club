using UnityEngine;
using Photon.Pun;

public class SyncPlayerContainer : MonoBehaviourPun, IPunObservable
{
    public PlayerContainer playerContainer; // PlayerContainer 참조

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // playerColor 동기화
            foreach (var kvp in playerContainer.playerColor)
            {
                string playerID = kvp.Key;
                Color color = kvp.Value;
                stream.SendNext(playerID);
                stream.SendNext(color.r); // 빨강 값 전송
                stream.SendNext(color.g); // 초록 값 전송
                stream.SendNext(color.b); // 파랑 값 전송
            }

            // playerScore 동기화
            foreach (var kvp in playerContainer.playerScore)
            {
                string playerID = kvp.Key;
                int score = kvp.Value;
                stream.SendNext(playerID);
                stream.SendNext(score);
            }

            // playerisDead 동기화
            foreach (var kvp in playerContainer.playerisDead)
            {
                string playerID = kvp.Key;
                bool isDead = kvp.Value;
                stream.SendNext(playerID);
                stream.SendNext(isDead);
            }
        }
        else
        {
            // playerColor 동기화
            while (stream.PeekNext() is string playerID)
            {
                float r = (float)stream.ReceiveNext(); // 빨강 값 수신
                float g = (float)stream.ReceiveNext(); // 초록 값 수신
                float b = (float)stream.ReceiveNext(); // 파랑 값 수신
                Color color = new Color(r, g, b); // RGB 값으로 색상 생성
                playerContainer.AddPlayerColor(playerID, color);
            }

            // playerScore 동기화
            while (stream.PeekNext() is string playerID)
            {
                int score = (int)stream.ReceiveNext();
                playerContainer.AddPlayerScore(playerID, score);
            }

            // playerisDead 동기화
            while (stream.PeekNext() is string playerID)
            {
                bool isDead = (bool)stream.ReceiveNext();
                playerContainer.AddPlayerisDead(playerID, isDead);
            }
        }
    }
}
