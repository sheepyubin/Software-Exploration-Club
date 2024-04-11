using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unexploded_mine : MonoBehaviour
{
    public mineModeManager mineModeManager;
    private int alliveMan;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            alliveMan++;
            mineModeManager.AddLive(alliveMan);
            Debug.Log("ºÒ¹ßÅº ¹âÀ½. »ýÁ¸");
        }
    }
}
