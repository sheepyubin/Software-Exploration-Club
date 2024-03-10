using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // PlayerContainer ����
    public Vector3 spawnPoint; // ���� ��ġ ���� ��

    private Color color;
    private SpriteRenderer spriteRenderer;
    private int playerNum;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady && photonView != null)
        {
            playerNum = PhotonNetwork.LocalPlayer.ActorNumber;

            color = new Color(Random.value, Random.value, Random.value);

            container.AddPlayerColor(playerNum, color);

            GameObject prefab = container.playerPrefab;

            spriteRenderer = prefab.GetComponent<SpriteRenderer>();

            spriteRenderer.color = container.ReturnPlayerColor(playerNum);

            container.AddPlayerData(playerNum, prefab);

            PhotonNetwork.Instantiate(prefab.name, spawnPoint, Quaternion.identity);

            container.ResetScore(playerNum);
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
