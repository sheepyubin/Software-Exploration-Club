using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class ScoreTextS2 : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public EndPointS2 endPointS2;

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        text.text = playerContainer.ReturnScore().ToString() + "/" +  endPointS2.targetScore;
    }
}
