using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1S1 : MonoBehaviour
{
    public Stage1Data data;  // Stage1Data ��ũ���ͺ� ������Ʈ

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾�� �浹
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #1");
            data.Setkey1(true); // Ű ȹ��
            Destroy(gameObject);
        }
    }
}
