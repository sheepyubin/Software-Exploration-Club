using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Test : MonoBehaviourPunCallbacks
{
    TextMeshProUGUI textMeshProUGUI;

    string text;

    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textMeshProUGUI.text = text;
    }

    public void Info(string ID, string isDead, string score)
    {
        text = ID + " " + isDead + " "+ score;
        
        Debug.Log(text);
    }
}
