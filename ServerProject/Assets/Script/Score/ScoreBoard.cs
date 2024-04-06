using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviourPun
{
    public float delayTIme = 3f;

    public Image[] userColor;
    public TextMeshProUGUI[] userScore;

    public PlayerContainer playerContainer;
    public ScoreData scoreData;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Score()
    {
        playerContainer.MergeIndex();

        int[] scoreArray = scoreData.ReturnScoreArray();

        for(int i=0; i<scoreArray.Length; i++)
        {
            userScore[i].text = scoreArray[i].ToString();
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
