using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer 참조
    public Vector3 spawnPoint; // 스폰 위치 벡터 값

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            // PlayerContainer에서 스폰 위치 인덱스를 가져옴
            int index = container.GetIndex();
            int playerNum = photonView.OwnerActorNr;

            container.RestoreNum(index, playerNum);
            // PlayerContainer에서 해당 인덱스에 있는 프리팹을 가져옴
            GameObject prefabToSpawn = container.playerPrefabs[index];

            // 프리팹을 생성
            PhotonNetwork.Instantiate(prefabToSpawn.name, spawnPoint, Quaternion.identity);
        }
    }
}
