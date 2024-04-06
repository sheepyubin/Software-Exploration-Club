using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private string playerID;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            playerID = PhotonNetwork.LocalPlayer.UserId; // �÷��̾� ID ����

            container.ResetContainer(playerID);

            GameObject prefab = container.playerPrefab; // �����̳��� �÷��̾� ������

            container.AddPlayerData(playerID, prefab); // �����̳��� playerData�� ����

            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity); // �÷��̾� ����
        }
    }
}
