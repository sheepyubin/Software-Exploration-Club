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


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Score()
    {        
        gameObject.SetActive(true);

        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.SetActive(false);
    }
}
