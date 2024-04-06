using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveS3 : MonoBehaviour
{
    public float speed = 10f; // ź��

    private Vector3 moveDirection; // �̵� ����

    void Update()
    {
        // �̵�
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // ������Ʈ �ı�
    void Destroy()
    {
        gameObject.SetActive(false);
    }

    // �浹 ó��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }

    private void OnEnable()
    {
        SetDirection();
        Invoke("Destroy", 3f);
    }

    // ���� �ʱ�ȭ
    void SetDirection()
    {
        moveDirection = transform.right; // ������ ���� �״��
    }
    // �÷��̾� �ǰ�
    void Damage()
    {
        Destroy();
    }
}
