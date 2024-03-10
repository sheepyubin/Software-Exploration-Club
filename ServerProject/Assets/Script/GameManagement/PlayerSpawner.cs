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
        int playerNum = PhotonNetwork.LocalPlayer.ActorNumber;

        GameObject prefab = playerContainer.ReturnPlayerData(playerNum);

        PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity);
    }
}
