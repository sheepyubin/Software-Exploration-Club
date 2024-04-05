using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.ComponentModel;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public PlayerContainer playerContainer; // PlayerContainer ��ũ���ͺ� ������Ʈ ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private string playerId;

    private void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.UserId;

        GameObject prefab = playerContainer.ReturnPlayerData(playerId); // prefab�� ���� �÷��̾��� �������� ����

        PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity); // ����
    }
}
