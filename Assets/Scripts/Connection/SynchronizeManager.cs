using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizeManager : MonoBehaviour
{
    public delegate void SyncRequest(string syncCode);
    public static event SyncRequest OnSyncRequest;

    public static void RaiseSyncRequest(string syncCode)
    {
        if(OnSyncRequest != null)
            OnSyncRequest.Invoke(syncCode);
    }

}
