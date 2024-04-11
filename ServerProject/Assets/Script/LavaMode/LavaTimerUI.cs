using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LavaTimerUI : MonoBehaviour
{
    [Header("Container:")]
    [SerializeField] private isDeadContainer isdeadContainer;
    [SerializeField] private ScoreContainer scoreContainer;


    [Header("Text UI:")]
    [SerializeField] private TextMeshProUGUI timerText; // TextMeshPro Text ���
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("End Popup:")]
    [SerializeField] private GameObject endPopUp;
    [SerializeField] private TextMeshProUGUI endRound;
    [SerializeField] private TextMeshProUGUI endScore;

    private float totalTime; // Ÿ�̸��� �� �ð�

    public int player;
    private GameDirector gameDirector;
    float currentTime;
    int round;
    int score = 0;
    int maxScore = 500;
    string move = "�� �� �����Դϴ�";
    string stop = "�� �� ����ϴ�";

    public bool isStop;
    private void Start()
    {
        isStop = false;
        gameDirector = GetComponent<GameDirector>();
        player = PhotonNetwork.CurrentRoom.PlayerCount;
        totalTime = gameDirector.modeChangeTime;
        scoreContainer.ResetScore();
        round = 1;
        endPopUp.SetActive(false);
        StartCoroutine(UpdateTimer());
        StartCoroutine(ScoreTimer());
    }
    IEnumerator UpdateTimer()
    {

        while (!isStop)
        {
            yield return new WaitForSeconds(1f); // 1�ʸ� ��ٸ��ϴ�.
            currentTime -= 1f; // 1�ʾ� ����

        }

    }
    IEnumerator ScoreTimer()
    {
        while (!isStop)
        {
            scoreContainer.AddScore(player);


            yield return new WaitForSeconds(0.1f); // 1�ʸ� ��ٸ��ϴ�.
        }
    }
    private void Update()
    {
        score = scoreContainer.ReturnScore();
        if (score >= maxScore)
        {
            ++round;
            maxScore += 500;
        }
        LavaMoveMode();
        LavaScore();
        LavaRound();
        if (player == 0)
        {
            IsStop();
            StartCoroutine(WaitBeforeDisconnectAndLoadScene());
        }
    }
    IEnumerator WaitBeforeDisconnectAndLoadScene()
    {
        // 1�� ���� ���
        yield return new WaitForSeconds(1.0f);

        // ��� �Ŀ� PhotonNetwork ���� ���� �� �� �̵�
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
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
    private void IsStop()
    {
        isStop = true;
        gameDirector.LavaModeIsStop();
        endRound.SetText(round + " Round");
        endScore.SetText("Score : " + score);
        endPopUp.SetActive(true);
        
    }
}
