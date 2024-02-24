using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // 포톤의 네트워크 설정
        PhotonNetwork.SendRate = 144;
        PhotonNetwork.SerializationRate = 15;
    }
}
