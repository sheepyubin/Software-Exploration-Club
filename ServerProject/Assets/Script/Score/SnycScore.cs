using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncScore: MonoBehaviourPunCallbacks
{
    [SerializeField]
    private ScoreContainer scoreContainer;

    private void Start()
    {
        // 로컬 플레이어가 소유한 스크립터블 오브젝트인 경우에만 동작
        if (photonView.IsMine)
        {
            // 스크립터블 오브젝트에 Observer 등록
            scoreContainer.OnScoreChanged += ScoreChangedHandler;
        }
    }

    // 스크립터블 오브젝트의 점수 변경 이벤트 처리
    private void ScoreChangedHandler(int newScore)
    {
        // 점수 변경 사항을 네트워크를 통해 모든 플레이어에게 전파
        photonView.RPC("UpdateScore", RpcTarget.All, newScore);
    }

    // 네트워크를 통해 점수를 업데이트하는 RPC 메서드
    [PunRPC]
    private void UpdateScore(int newScore)
    {
        // 스크립터블 오브젝트의 점수를 업데이트
        scoreContainer.AddScore(newScore);
        Debug.Log("SnycScore: " + newScore);
    }
}
