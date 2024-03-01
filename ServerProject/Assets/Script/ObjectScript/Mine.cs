using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{


    public GameObject bommEffect;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.MineExplosion);
            Boom();
        }
    }

    void Boom()
    {
        Instantiate(bommEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
