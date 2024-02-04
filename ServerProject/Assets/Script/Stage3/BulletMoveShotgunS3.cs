using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveShotgunS3 : MonoBehaviour
{
    public float speed = 10f; // 탄속

    private Vector3 moveDirection; // 이동 방향

    void Start()
    {
        Invoke("Destroy", 3f);

        moveDirection = transform.right; // 생성된 방향 그대로
    }

    void Update()
    {
        // 이동
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
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

    // 플레이어 피격
    void Damage()
    {
        Debug.Log("Damage(Shotgun)");
        Destroy(gameObject);
    }
}
