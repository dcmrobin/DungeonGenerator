using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Lift : NetworkBehaviour
{
    public Transform liftGround;
    public NetworkVariable<bool> hasDescended = new NetworkVariable<bool>();
    public bool descending;
    public bool ascending;

    [ServerRpc(RequireOwnership = false)]
    public void ActivateLiftServerRpc()
    {
        if (!hasDescended.Value)
        {
            hasDescended.Value = true;
        }
        else if (hasDescended.Value)
        {
            hasDescended.Value = false;
        }
    }

    private void Update() {
        if (descending && liftGround.localPosition.y > -39.5)
        {
            liftGround.localPosition = Vector3.MoveTowards(liftGround.localPosition, new Vector3(0, -39.5f, 30), 1);
        }
        else if (ascending && liftGround.localPosition.y < 0.5)
        {
            liftGround.localPosition = Vector3.MoveTowards(liftGround.localPosition, new Vector3(0, 0.5f, 30), 1);
        }

        if (hasDescended.Value)
        {
            ascending = false;
            descending = true;
        }
        else if (!hasDescended.Value)
        {
            descending = false;
            ascending = true;
        }
    }
}
