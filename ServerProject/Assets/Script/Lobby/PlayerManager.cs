using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer 참조
    public Vector3 spawnPoint; // 스폰 위치 벡터 값

    private Color color;
    private SpriteRenderer spriteRenderer;
    private string playerID;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            playerID = PhotonNetwork.LocalPlayer.UserId; // 플레이어 ID 설정

            GameObject prefab = container.playerPrefab; // 컨테이너의 플레이어 프리팹

            color = new Color(Random.value, Random.value, Random.value); // 랜덤 색상 설정

            spriteRenderer = prefab.GetComponent<SpriteRenderer>(); // 플레이어 프리팹의 스프라이트 랜더러 접근

            spriteRenderer.color = color; // 플레이어 프리팹의 색상 변경

            container.AddPlayerData(playerID, prefab); // 컨테이너의 playerData에 저장

            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity);

            container.ResetScore(playerID);
        }
    }
}
