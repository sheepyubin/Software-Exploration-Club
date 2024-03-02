using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System.ComponentModel;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public PlayerContainer playerContainer; // PlayerContainer 스크립터블 오브젝트 참조
    public Vector3 spawnPoint; // 스폰 위치 벡터 값

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
