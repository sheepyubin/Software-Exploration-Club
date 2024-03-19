using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class mine_spwaner : MonoBehaviour
{
    public int fakeMine = 4;
    public int m = -8;
    [SerializeField]
    private GameObject unexploded_mine;
    private void Start()
    {
        for (int i = 0; i < fakeMine; ++i)
        {
            Instantiate(unexploded_mine, new Vector2(m, -31), Quaternion.identity);
            m += 4;
        }
    }


    void Update()
    {

    }
}