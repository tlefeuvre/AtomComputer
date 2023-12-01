using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu(menuName = "ScriptableObjects/Architecture")]

public class Architecture : ScriptableObject
{
    public static Architecture Instance { get; private set; }


    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        Instance = Resources.Load<Architecture>("Levels");
    }
    public List<FileTemplate> filesList;

}
