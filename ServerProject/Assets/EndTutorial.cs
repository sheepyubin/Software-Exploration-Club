using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EndTutorial : MonoBehaviour
{
     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.CompareTag("Player"))
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel("Lobby");
        }
     }
}
