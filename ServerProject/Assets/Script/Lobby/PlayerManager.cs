using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private Color color;
    private SpriteRenderer spriteRenderer;
    private string playerID;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            playerID = PhotonNetwork.LocalPlayer.UserId; // �÷��̾� ID ����

            GameObject prefab = container.playerPrefab; // �����̳��� �÷��̾� ������

            color = new Color(Random.value, Random.value, Random.value); // ���� ���� ����

            spriteRenderer = prefab.GetComponent<SpriteRenderer>(); // �÷��̾� �������� ��������Ʈ ������ ����

            spriteRenderer.color = color; // �÷��̾� �������� ���� ����

            container.AddPlayerData(playerID, prefab); // �����̳��� playerData�� ����

            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity);

            container.ResetScore(playerID);
        }
    }
}
