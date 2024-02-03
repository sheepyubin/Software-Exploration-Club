using UnityEngine;

public class BulletMoveS3 : MonoBehaviour
{
    public float speed = 10f; // 탄속
    public float damageRadius = 1f; // 피격 반경

    private Transform target; // 플레이어 위치
    private Vector3 moveDirection; // 이동 방향

    void Start()
    {
        // 타겟 설정
        FindPlayerTarget();

        // 타겟이 NULL이라면
        if (target == null)
        {
            Debug.LogWarning("Bullet target is NULL!.");
            return;
        }

        // 타겟 방향으로 설정
        moveDirection = (target.position - transform.position).normalized;


    }

    void Update()
    {
        Invoke("Destroy", 3f);
        // 이동
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // 플레이어 탐색
    void FindPlayerTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }

    // 플레이어 피격
    void Damage()
    {
        Debug.Log("Damage");
        Destroy(gameObject);
    }

    // 오브젝트 파괴
    void Destroy()
    {
        Destroy(gameObject);
    }

    // 충돌 처리
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }
}
