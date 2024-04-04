using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;
using UnityEngine;



public class mineModeManager : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public PlayerSpawner playerSpawner;
    string playerId;

    public int round = 1;
    public float time = 5;
    bool allDead = false;
    public string player1;
    public string player2;
    public string player3;
    public string player4;
    bool isReloadingScene = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerId = PhotonNetwork.LocalPlayer.UserId;
        if (round < 1)
        {
            round = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckAlive();
        
    }

    public void CheckAlive()
    {
        if (!isReloadingScene && playerContainer.ReturnPlayerisDeadAll())
        {
            isReloadingScene = true;
            StartCoroutine(WaitForReload());
        }
    }

    IEnumerator WaitForReload()
    {
        Debug.Log("ÀüºÎ »ç¸Á");
        yield return new WaitForSeconds(time);
        round++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int RealMineReturn()
    {
        return round;
    }
}
