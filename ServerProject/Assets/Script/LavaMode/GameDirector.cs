using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviourPunCallbacks
{
    [Header("PlayerContainer:")]
    [SerializeField] private isDeadContainer isdeadContainer;
    [SerializeField] private ScoreContainer scoreContainer;

    [Header("GameObject:")]
    [SerializeField] private GameObject platformPrefab; // 생성할 프리팹
    [SerializeField] private GameObject gearObject;
    [SerializeField] private Animator animator;

    [Header("RandomPosition:")]
    [SerializeField] private Vector2 minPosition; // 최소 위치
    [SerializeField] private Vector2 maxPosition; // 최대 위치

    [Header("Timer:")]
    [SerializeField] private float span = 2.0f;
    public float modeChangeTime = 3.0f;

    [Header("Object Option:")]
    [SerializeField] private float speed = 3.0f;

    private LavaTimerUI lavaTimerUI;
    private float delta = 0.0f;
    public bool moveMode;
    public bool isDead;
    private void Start()
    {
        lavaTimerUI = GetComponent<LavaTimerUI>();
        isDead = false;
        moveMode = false;
        maxPosition.y = minPosition.y + 10;
        isdeadContainer.AddisDead();
        StartCoroutine(SwichingMode());
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetMoveModeRPC", RpcTarget.AllBuffered, moveMode);
        }
    }
    [PunRPC]
    private void SetMoveModeRPC(bool mode)
    {
        moveMode = mode;
        MoveMode(moveMode);
    }
    IEnumerator SwichingMode()
    {
        while (!lavaTimerUI.isStop)
        {
            
            if (moveMode)
            {
                moveMode = false;
                maxPosition.y = minPosition.y + 10;
            }
            else
            {
                moveMode = true;
                maxPosition.y = minPosition.y;
            }
            yield return new WaitForSeconds(modeChangeTime);
        
        }
    }
    // 랜덤 좌표 생성 및 프리팹 생성 함수
    private void SpawnRandomPrefab()
    {
        // 랜덤한 좌표 생성
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector3 randomPosition = new Vector3(randomX, randomY);

        // 프리팹 생성
        Instantiate(platformPrefab, transform.position + randomPosition, Quaternion.identity);
    }
    private void MoveMode(bool mode)
    {
        if (mode)
            Movement(this.speed);
        else
            Movement(0);
    }
    private void Movement(float speed)
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        gearObject.transform.Translate(0, speed * Time.deltaTime, 0);
        animator.SetFloat("isRotationSpeed", speed);
    }

    // 예시로 매 프레임마다 랜덤 프리팹을 생성하는 Update 함수
    void Update()
    {
        if (!lavaTimerUI.isStop)
        {
            this.delta += Time.deltaTime;
            if (delta > span)
            {
                delta = 0.0f;
                SpawnRandomPrefab(); // 랜덤 프리팹 생성
            }
            MoveMode(moveMode);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dead");
            --lavaTimerUI.player;
        }
    }
    public void LavaModeIsStop()
    {
        Movement(0);
    }
}