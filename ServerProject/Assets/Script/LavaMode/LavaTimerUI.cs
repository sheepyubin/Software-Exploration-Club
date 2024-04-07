using System.Collections;
using UnityEngine;
using TMPro;

public class LavaTimerUI : MonoBehaviour
{
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] private TMP_Text timerText; // TextMeshPro Text 요소

    private float totalTime; // 타이머의 총 시간

    private void Start()
    {
        totalTime = gameDirector.modeChangeTime;
        StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        float currentTime = totalTime;

        while (currentTime > 0)
        {
            timerText.text = currentTime.ToString("F0") + "초 후 움직입니다"; // 소수점 첫째 자리까지 표시

            yield return new WaitForSeconds(1f); // 1초를 기다립니다.
            currentTime -= 1f; // 1초씩 감소
        }

        // 타이머가 종료되면 0으로 표시하거나 다른 처리를 수행할 수 있습니다.
        timerText.text = "0.0";
    }
}
