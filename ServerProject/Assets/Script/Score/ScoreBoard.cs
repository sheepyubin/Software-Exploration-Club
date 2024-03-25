using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public float delayTIme = 3f;

    public Image[] userColor;
    public TextMeshProUGUI[] userScore;

    public PlayerContainer playerContainer;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Score()
    {
        for (int i = 0; i < userScore.Length; i++) // userScore�� �÷��̾��� ������ ����
        {
            int score = playerContainer.ReturnPlayerScoreArray(i);

            if (score == 0)
                userScore[i].text = "0";
            else
                userScore[i].text = score.ToString();
        }

        for (int i = 0; i < userColor.Length; i++) // userColor�� �÷��̾��� ������ ����
        {
            Color color = playerContainer.ReturnPlayerColorArray(i);

            if (color != Color.white)
                userColor[i].color = color;
        }

        gameObject.SetActive(true);

        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.SetActive(false);
    }
}
