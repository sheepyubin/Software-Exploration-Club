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
    [SerializeField] private TextMeshProUGUI timerText; // TextMeshPro Text 요소
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("End Popup:")]
    [SerializeField] private GameObject endPopUp;
    [SerializeField] private TextMeshProUGUI endRound;
    [SerializeField] private TextMeshProUGUI endScore;

    private float totalTime; // 타이머의 총 시간

    public int player;
    private GameDirector gameDirector;
    float currentTime;
    int round;
    int score = 0;
    int maxScore = 500;
    string move = "초 후 움직입니다";
    string stop = "초 후 멈춥니다";

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
            yield return new WaitForSeconds(1f); // 1초를 기다립니다.
            currentTime -= 1f; // 1초씩 감소

        }

    }
    IEnumerator ScoreTimer()
    {
        while (!isStop)
        {
            scoreContainer.AddScore(player);


            yield return new WaitForSeconds(0.1f); // 1초를 기다립니다.
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
        // 1초 동안 대기
        yield return new WaitForSeconds(1.0f);

        // 대기 후에 PhotonNetwork 연결 해제 및 씬 이동
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
