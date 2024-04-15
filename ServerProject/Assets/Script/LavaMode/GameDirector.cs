using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [Header("PlayerContainer:")]
    [SerializeField] private isDeadContainer isdeadContainer;
    [SerializeField] private ScoreContainer scoreContainer;

    [Header("GameObject:")]
    [SerializeField] private GameObject platformPrefab; // ������ ������
    [SerializeField] private GameObject gearObject;
    [SerializeField] private Animator animator;

    [Header("RandomPosition:")]
    [SerializeField] private Vector2 minPosition; // �ּ� ��ġ
    [SerializeField] private Vector2 maxPosition; // �ִ� ��ġ

    [Header("Timer:")]
    [SerializeField] private float span = 2.0f;
    public float modeChangeTime = 3.0f;

    [Header("Object Option:")]
    [SerializeField] private float speed = 3.0f;

    public LavaTimerUI lavaTimerUI;
    private float delta = 0.0f;
    public bool moveMode;
    public bool isDead;
    private void Start()
    {
        isDead = false;
        moveMode = false;
        maxPosition.y = minPosition.y + 10;
        isdeadContainer.AddisDead();
        StartCoroutine(SwichingMode());
        Random.InitState(1234);
    }
    IEnumerator SwichingMode()
    {
        while (!lavaTimerUI.isStop)
        {
            
            if (moveMode)
            {
                lavaTimerUI.isAddScore = true;
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
    // ���� ��ǥ ���� �� ������ ���� �Լ�
    private void SpawnRandomPrefab()
    {
        // ������ ��ǥ ����
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector3 randomPosition = new Vector3(randomX, randomY);

        // ������ ����
        Instantiate(platformPrefab, transform.position + randomPosition, Quaternion.identity);
    }
    private void MoveMode(bool mode)
    {
        if (mode)
            Movement(0);
        else
            Movement(this.speed);
    }
    private void Movement(float speed)
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        gearObject.transform.Translate(0, speed * Time.deltaTime, 0);
        animator.SetFloat("isRotationSpeed", speed);
    }

    // ���÷� �� �����Ӹ��� ���� �������� �����ϴ� Update �Լ�
    void Update()
    {
        if (!lavaTimerUI.isStop)
        {
            this.delta += Time.deltaTime;
            if (delta > span)
            {
                delta = 0.0f;
                SpawnRandomPrefab(); // ���� ������ ����
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