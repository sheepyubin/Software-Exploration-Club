using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{

    public Text scoreText;
    private Image clearUI;
    public RoundData roundData;
    private float countdownTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        scoreText.text = string.Format("  ");
    }

    // Update is called once per frame
    void Update()
    {
        if (roundData.ReturnRound() > 5)
        {
            gameObject.SetActive(true);
            countdownTime = roundData.ReturnTime();

            int seconds = Mathf.FloorToInt(countdownTime);
            int milliseconds = Mathf.FloorToInt((countdownTime - seconds) * 100);
            scoreText.text = string.Format("{0}.{1:D2}", seconds, milliseconds);
        }
    }
}
