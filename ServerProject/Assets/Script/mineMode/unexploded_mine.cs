using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unexploded_mine : MonoBehaviour
{
    public RoundData roundData;
    private int alliveMan;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            alliveMan++;
            roundData.AddDead(alliveMan);
            Debug.Log("ºÒ¹ßÅº ¹âÀ½. »ýÁ¸");
        }
    }
}
