using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class mine_spwaner : MonoBehaviour
{
    public mineModeManager mineModeManager;
    public int totalMine = 5;

    public int fakeMine = 4;
    public int realMine = 1;
    public int mine_distance = -8;
    private int now_mine=1;
    public int realmineCount;

   [SerializeField]
    public GameObject unexploded_mine;
    public GameObject real_mine;
    private void Start()
    {
        realMine = mineModeManager.RealMineReturn();
        Debug.Log("��¥ ���� " + realMine +"��");
        for (int i = 0; i < realMine; ++i)
        {
            realmineCount = Random.Range(1, 6);
        }

        for (int i = 0; i < totalMine; ++i)
        {
            if (now_mine != realmineCount)
            {
                Instantiate(unexploded_mine, new Vector2(mine_distance, -31), Quaternion.identity);
                mine_distance += 4;
                now_mine++;
            }
            else
            {
                Instantiate(real_mine, new Vector2(mine_distance, -31), Quaternion.identity);
                mine_distance += 4;
                now_mine++;
            }
        }
    }


    void Update()
    {

    }
}