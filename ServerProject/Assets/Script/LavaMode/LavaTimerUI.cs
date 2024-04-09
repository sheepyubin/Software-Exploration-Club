using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LavaTimerUI : MonoBehaviour
{
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] private TMP_Text timerText; // TextMeshPro Text 요소

    private float totalTime; // 타이머의 총 시간

    float currentTime;
    string move = "초 후 움직입니다";
    string stop = "초 후 멈춥니다";
    private void Start()
    {
        totalTime = gameDirector.modeChangeTime;
        StartCoroutine(UpdateTimer());
    }
    IEnumerator UpdateTimer()
    {

        while (true)
        {
            if (currentTime == 0.0f)
                currentTime = totalTime;
            if (!gameDirector.moveMode) 
            {
                timerText.text = currentTime.ToString("F0") + move;
            }
            else
            {
                timerText.text = currentTime.ToString("F0") + stop;
            }

            yield return new WaitForSeconds(1f); // 1초를 기다립니다.
            currentTime -= 1f; // 1초씩 감소
        }

    }
}
