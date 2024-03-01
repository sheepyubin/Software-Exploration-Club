using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    AudioSource audioSource; // AudioSource�� ����

    public GameObject bommEffect;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // ��ȯ ���� ����
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            Boom();
        }
    }

    void Boom()
    {
        Instantiate(bommEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
