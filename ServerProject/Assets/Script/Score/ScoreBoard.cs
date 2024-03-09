using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public float delayTIme = 3f;

    public void Score()
    {
        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        gameObject.SetActive(false);
    }
}
