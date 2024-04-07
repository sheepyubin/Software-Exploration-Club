using System.Collections;
using UnityEngine;
using TMPro;

public class LavaTimerUI : MonoBehaviour
{
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] private TMP_Text timerText; // TextMeshPro Text ���

    private float totalTime; // Ÿ�̸��� �� �ð�

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
            timerText.text = currentTime.ToString("F0") + "�� �� �����Դϴ�"; // �Ҽ��� ù° �ڸ����� ǥ��

            yield return new WaitForSeconds(1f); // 1�ʸ� ��ٸ��ϴ�.
            currentTime -= 1f; // 1�ʾ� ����
        }

        // Ÿ�̸Ӱ� ����Ǹ� 0���� ǥ���ϰų� �ٸ� ó���� ������ �� �ֽ��ϴ�.
        timerText.text = "0.0";
    }
}
