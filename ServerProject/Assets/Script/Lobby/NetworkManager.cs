using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // ������ ��Ʈ��ũ ����
        PhotonNetwork.SendRate = 144;
        PhotonNetwork.SerializationRate = 15;
    }
}
