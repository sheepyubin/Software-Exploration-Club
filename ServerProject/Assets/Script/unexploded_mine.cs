using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unexploded_mine : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ºÒ¹ßÅº ¹âÀ½. »ýÁ¸");
        }
    }
}
