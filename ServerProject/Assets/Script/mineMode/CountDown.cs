using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countdownText;

    private float countdownTime = 40f;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (countdownTime > 0f)
        {
            int seconds = Mathf.FloorToInt(countdownTime);
            int milliseconds = Mathf.FloorToInt((countdownTime - seconds) * 100);
            countdownText.text = string.Format("{0}.{1:D2}", seconds, milliseconds);

            yield return new WaitForSeconds(0.01f);
            countdownTime -= 0.01f;
        }

        countdownText.text = "카운트다운 종료!";
    }
}
