using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCodeManager : MonoBehaviour
{
    SeverLauncher serverlaucher; // 서버 런처 스크립트

    public string lobbyCode; // 로비 코드

    private int creationCount = 0; // 오브젝트 생성 횟수
    private void Start()
    {
        if (creationCount == 0)
        {
            // DontDestroyOnLoad 매서드가 한 번만 호출되게 함
            // 여러번 호출 시 LobbyCodeManager가 여러개로 늘어나서 로비 코드 버그남
            DontDestroy();
            creationCount++;
        }
        serverlaucher = GameObject.Find("SeverLauncher").GetComponent<SeverLauncher>(); // 서버 런처 스크립트 접근
    }

    private void Update()
    {
        lobbyCode = serverlaucher.lobbyCode;
    }

    private void DontDestroy() // DontDestroyOnLoad 매서드 호출 매서드
    {
        DontDestroyOnLoad(gameObject);
    }
}