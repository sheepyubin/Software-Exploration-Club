using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMine : MonoBehaviour
{
    public GameObject bommEffect;
    public RoundData roundData;
    private int deadman;

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Boom();
            deadman++;
            roundData.AddDead(deadman);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.MineExplosion);
        }
    }

    void Boom()
    {
        Instantiate(bommEffect, transform.position, Quaternion.identity);
    }
}
