using UnityEngine;

[CreateAssetMenu(fileName = "Stage1Data", menuName = "ScriptableObjects/Stage1Data", order = 1)]
public class Stage1Data : ScriptableObject
{
    private bool key1 = false; // 1�� Ű�� ����°�?
    private bool key2 = false; // 2�� Ű�� ����°�?
    private bool isClear = false; // ���������� Ŭ���� �ߴ°�?
    
    public void Setkey1(bool set)
    {
        key1 = set;
        Debug.Log("Stage1data.key1" + key1);
    }

    public bool Returnkey1()
    {
        return key1;
    }

    public void Setkey2(bool set)
    {
        key2 = set;
        Debug.Log("Stage1data.key2" + key1);
    }

    public bool Returnkey2()
    {
        return key2;
    }

    public void SetisClear(bool set)
    {
        isClear = set;
    }

    public bool ReturnisClear()
    {
        return isClear;
    }
}
