using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomMine : MonoBehaviour
{
    public GameObject bommEffect;
    public mineModeManager mineModeManager;
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
            mineModeManager.AddDead(deadman);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.MineExplosion);
            Debug.Log("È°¼ºÈ­µÈ Áö·Ú - »ç¸Á");
        }
    }

    void Boom()
    {
        Instantiate(bommEffect, transform.position, Quaternion.identity);
    }
}
