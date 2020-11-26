using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public GridH playerGrid;

    [Command]
    public void CmdSend(string message)
    {
        if (message != "")
            RpcReceive(message);
    }

    [ClientRpc]
    public void RpcReceive(string message)
    {
        GridSaveData data = JsonUtility.FromJson<GridSaveData>(message);
        playerGrid?.Load(data);
    }

    public void Send(string data)
    {
        CmdSend(data);
    }

}
