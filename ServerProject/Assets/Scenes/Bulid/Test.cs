using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Test : MonoBehaviourPunCallbacks
{
    TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        textMeshProUGUI.text = PhotonNetwork.LocalPlayer.UserId;
    }
}
