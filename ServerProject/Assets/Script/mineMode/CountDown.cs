using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countdownText;
    public RoundData roundData;
    public PlayerContainer playerContainer;

    private float countdownTime = 0f;

    void Awake()
    {
        countdownTime = roundData.ReturnTime();
        countdownTime = roundData.ReturnTime();
        countdownTime = roundData.ReturnTime();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (countdownTime > -1f)
        {
            if (playerContainer.ReturnPlayerisDeadAll() == true || roundData.ReturnClear() == true)
            {
                roundData.AddTime(countdownTime);
            }

            int seconds = Mathf.FloorToInt(countdownTime);
            int milliseconds = Mathf.FloorToInt((countdownTime - seconds) * 100);
            countdownText.text = string.Format("{0}.{1:D2}", seconds, milliseconds);

            yield return new WaitForSeconds(0.01f);
            countdownTime += 0.01f;
        }
    }
}
