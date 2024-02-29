using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject endflag;

    private static Score GetScore;
    void Start()
    {
        GetScore= gameObject.GetComponent<Score>();
        Debug.Log("Ω√¿€");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callScore(int actorNumber)
    {
        GetScore.UpdateScore();
    }
}
