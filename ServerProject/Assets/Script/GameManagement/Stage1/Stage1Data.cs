using UnityEngine;

[CreateAssetMenu(fileName = "Stage1Data", menuName = "ScriptableObjects/Stage1Data", order = 1)]
public class Stage1Data : ScriptableObject
{
    private bool key1 = false; // 1번 키를 얻었는가?
    private bool key2 = false; // 2번 키를 얻었는가?
    private bool isClear = false; // 스테이지를 클리어 했는가?
    
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
