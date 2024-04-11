using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Photon.Pun;
using Photon.Realtime;

public class LavaTimerUI : MonoBehaviour
{
    [Header("PlayerContainer:")]
    [SerializeField] private PlayerContainer playerContainer;
    

    
    [Header("TextUI:")]
    [SerializeField] private TextMeshProUGUI timerText; // TextMeshPro Text ���
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float totalTime; // Ÿ�̸��� �� �ð�

    public int player;
    private GameDirector gameDirector;
    float currentTime;
    int round;
    int score = 0;
    int maxScore = 500;
    string move = "�� �� �����Դϴ�";
    string stop = "�� �� ����ϴ�";
    private void Start()
    {
        gameDirector = GetComponent<GameDirector>();
        player = PhotonNetwork.CurrentRoom.PlayerCount;
        totalTime = gameDirector.modeChangeTime;
        playerContainer.scoreContainer.ResetScore();
        round = 1;
        StartCoroutine(UpdateTimer());
        StartCoroutine(ScoreTimer());
    }
    IEnumerator UpdateTimer()
    {

        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�ʸ� ��ٸ��ϴ�.
            currentTime -= 1f; // 1�ʾ� ����

        }

    }
    IEnumerator ScoreTimer()
    {
        while (true)
        {
            playerContainer.scoreContainer.AddScore(player);


            yield return new WaitForSeconds(0.1f); // 1�ʸ� ��ٸ��ϴ�.
        }
    }
    private void Update()
    {
        score = playerContainer.scoreContainer.ReturnScore();
        if (score >= maxScore)
        {
            ++round;
            maxScore += 500;
        }
        LavaMoveMode();
        LavaScore();
        LavaRound();
    }
    private void LavaMoveMode()
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
    }
    private void LavaScore()
    {
        scoreText.SetText(score + " / " + maxScore);
    }
    private void LavaRound()
    {
        roundText.SetText(round + " Round");
    }
}
