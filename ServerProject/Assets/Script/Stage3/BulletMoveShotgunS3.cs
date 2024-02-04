using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveShotgunS3 : MonoBehaviour
{
    public float speed = 10f; // ź��

    private Vector3 moveDirection; // �̵� ����

    void Start()
    {
        Invoke("Destroy", 3f);

        moveDirection = transform.right; // ������ ���� �״��
    }

    void Update()
    {
        // �̵�
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // ������Ʈ �ı�
    void Destroy()
    {
        Destroy(gameObject);
    }

    // �浹 ó��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }

    // �÷��̾� �ǰ�
    void Damage()
    {
        Debug.Log("Damage(Shotgun)");
        Destroy(gameObject);
    }
}
