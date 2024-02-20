using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.ComponentModel;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public PlayerContainer playerContainer; // PlayerContainer ��ũ���ͺ� ������Ʈ ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private void Start()
    {
        int playerNum = photonView.OwnerActorNr;

        int index = playerContainer.GetNum(playerNum);

        GameObject prefabToSpawn = playerContainer.playerPrefabs[index];

        PhotonNetwork.Instantiate(prefabToSpawn.name, spawnPoint, Quaternion.identity);
    }
}
