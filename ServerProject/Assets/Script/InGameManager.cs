using UnityEngine;
using Photon.Pun;

public class InGameManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;

    void Start()
    {
        // ���� �÷��̾� ����
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Vector2 spawnPosition = new Vector2(0f, 0f);
            PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
    }
}

