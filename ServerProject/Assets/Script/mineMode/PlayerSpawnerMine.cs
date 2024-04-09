using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnerMine : MonoBehaviour
{
    public PlayerContainer playerContainer; // PlayerContainer ��ũ���ͺ� ������Ʈ ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private string playerId;

    private void Start()
    {
        playerId = PhotonNetwork.LocalPlayer.UserId;

        GameObject prefab = playerContainer.ReturnPlayerData(playerId); // prefab�� ���� �÷��̾��� �������� ����

        if (!playerContainer.ReturnPlayerisDead(playerId))
        {
            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity); // ����
        }
    }
}
