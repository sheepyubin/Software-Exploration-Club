using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")) // Boundary �±׿� ��Ҵ°�?
        {
            transform.position = new Vector3(0,0, 0);
        }
    }
}
