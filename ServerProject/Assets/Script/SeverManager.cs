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
        // ���� ���� ���� �� �ݹ�
        Debug.Log("���� ����");
    }
}
