using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary")) // Boundary 태그에 닿았는가?
        {
            transform.position = new Vector3(0,0, 0);
        }
    }
}
