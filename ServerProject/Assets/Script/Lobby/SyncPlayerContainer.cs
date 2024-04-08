using UnityEngine;
using Photon.Pun;

public class SyncPlayerContainer : MonoBehaviourPun
{
    public PlayerContainer playerContainer;

    [PunRPC]

    void SyncPlayerColor(string playerID, float r, float g, float b)
    {
        Color color = new Color(r, g, b);

        playerContainer.AddPlayerColor(playerID, color);
    }

    [PunRPC]
    
    void SyncPlayerIsDead(string playerID, bool isDead)
    {
        playerContainer.AddPlayerisDead(playerID, isDead);

        Debug.Log("playerID: " + playerID + "\nisDead: " + playerContainer.ReturnPlayerisDead(playerID));
    }
}
