using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1S1 : MonoBehaviour
{
    public KeyDataS1 keyData;  // �������� 1�� KeyData ��ũ���ͺ� ������Ʈ

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾�� �浹
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #1");
            keyData.Key1 = true; // Ű ȹ��
            Destroy(gameObject);
        }
    }
}
