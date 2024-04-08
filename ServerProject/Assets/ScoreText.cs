using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class ScoreText : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public EndPointS1 endPointS1;

    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        text.text = playerContainer.ReturnScore().ToString() + "/" +  endPointS1.targetScore;
    }
}
