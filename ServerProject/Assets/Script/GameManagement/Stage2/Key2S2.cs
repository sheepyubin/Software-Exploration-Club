using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2S2 : MonoBehaviour
{
    public Stage2Data data; // �������� 1�� KeyData ��ũ���ͺ� ������Ʈ

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾�� �浹
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #2");
            data.Setkey2(true); // Ű ȹ��
            Destroy(gameObject);
        }
    }
}
