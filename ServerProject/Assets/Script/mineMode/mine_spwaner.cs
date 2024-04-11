using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine_spwaner : MonoBehaviour
{
    public mineModeManager mineModeManager;
    public int totalMine = 5;
    private int realMine;

    public GameObject unexploded_mine;
    public GameObject real_mine;

    private int seed;

    private void Start()
    {
        StartCoroutine(WaitForReload());

        realMine = mineModeManager.RealMineReturn();
        Debug.Log("��¥ ���� " + realMine + "��");

        List<GameObject> minePrefabs = new List<GameObject>();

        for (int i = 0; i < realMine; i++)
        {
            minePrefabs.Add(real_mine);
        }

        for (int i = realMine; i < totalMine; i++)
        {
            minePrefabs.Add(unexploded_mine);
        }

        // �÷��̾�� ������ ���� �õ� ���
        Random.InitState(seed); // ������ �õ� �� (���ϴ� ������ ���� ����)

        Shuffle(minePrefabs);

        int mine_distance = -8;
        foreach (GameObject prefab in minePrefabs)
        {
            Instantiate(prefab, new Vector2(mine_distance, -29), Quaternion.identity);
            mine_distance += 4;
        }
    }

    int GenerateRandomNumber()
    {

        System.Random random = new System.Random();
        return random.Next(1000, 10000);
    }
    private void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(1f);
    }
}
