using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LavaTimerUI : MonoBehaviour
{
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] private TMP_Text timerText; // TextMeshPro Text ���

    private float totalTime; // Ÿ�̸��� �� �ð�

    float currentTime;
    string move = "�� �� �����Դϴ�";
    string stop = "�� �� ����ϴ�";
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

            yield return new WaitForSeconds(1f); // 1�ʸ� ��ٸ��ϴ�.
            currentTime -= 1f; // 1�ʾ� ����
        }

    }
}
