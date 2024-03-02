using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.ComponentModel;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public PlayerContainer playerContainer; // PlayerContainer ��ũ���ͺ� ������Ʈ ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        int playerNum = photonView.OwnerActorNr;
        Color color = playerContainer.ReturnPlayerColor(playerNum);
        GameObject prefab = playerContainer.playerPrefab;

        spriteRenderer = prefab.GetComponent<SpriteRenderer>();

        spriteRenderer.color = color;

        PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity);
    }
}
