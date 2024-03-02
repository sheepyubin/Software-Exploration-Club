using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private Color color;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            color = new Color(Random.value, Random.value, Random.value);

            container.AddPlayerData(photonView.OwnerActorNr, color);

            GameObject prefab = container.playerPrefab;

            spriteRenderer = prefab.GetComponent<SpriteRenderer>();

            spriteRenderer.color = color;

            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity);
        }

        //// PlayerContainer���� ���� ��ġ �ε����� ������
        //int index = container.GetIndex();
        //int playerNum = photonView.OwnerActorNr;

        //container.RestoreNum(index, playerNum);
        //// PlayerContainer���� �ش� �ε����� �ִ� �������� ������
        //GameObject prefabToSpawn = container.playerPrefabs[index];

        //// �������� ����
        //PhotonNetwork.Instantiate(prefabToSpawn.name, spawnPoint, Quaternion.identity);
    }
    }
