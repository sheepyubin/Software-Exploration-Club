using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer 참조
    public Vector3 spawnPoint; // 스폰 위치 벡터 값

    private string playerID;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            playerID = PhotonNetwork.LocalPlayer.UserId; // 플레이어 ID 설정

            GameObject prefab = container.playerPrefab; // 컨테이너의 플레이어 프리팹

            container.AddPlayerData(playerID, prefab); // 컨테이너의 playerData에 저장

            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity); // 플레이어 생성
        }
    }
}
