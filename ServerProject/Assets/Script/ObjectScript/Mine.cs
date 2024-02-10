using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Boom");
            Damage();
        }
    }
    void Destroy()
    {
        gameObject.SetActive(false);
    }
    void Damage()
    {
        Debug.Log("Damage(Mine)");
        Destroy();
    }
}
