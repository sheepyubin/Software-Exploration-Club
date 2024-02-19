using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // PlayerContainer���� ���� ��ġ �ε����� ������
            int index = container.GetIndex();

            // PlayerContainer���� �ش� �ε����� �ִ� �������� ������
            GameObject prefabToSpawn = container.playerPrefabs[index];

            // �������� ����
            PhotonNetwork.Instantiate(prefabToSpawn.name, spawnPoint, Quaternion.identity);
        }
    }
}
