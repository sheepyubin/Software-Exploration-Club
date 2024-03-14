using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.ComponentModel;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public PlayerContainer playerContainer; // PlayerContainer 스크립터블 오브젝트 참조
    public Vector3 spawnPoint; // 스폰 위치 벡터 값

    private string playerId;
    private void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.UserId;

        GameObject prefab = playerContainer.ReturnPlayerData(playerId); // prefab에 로컬 플레이어의 프리팹을 저장

        PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity); // 생성
    }
}
