using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SeverManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // 포톤 서버 연결 시 콜백
        Debug.Log("서버 연결");
    }
}
