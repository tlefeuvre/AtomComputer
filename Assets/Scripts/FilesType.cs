using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FileType
{
    FOLDER,
    TEXT,
    AUDIO,
    LOCK,
    IMAGE,
    SUCCESS
}

[System.Serializable]
public class FileTemplate
{
    public string name;
    public string textFileName;
    public string textContent;
    public string password;
    public int depthIndex;
    public bool canBeUnlock;
    public bool unlockAll;
    public AudioClip audioContent;
    public Sprite imageContent;
    public FileType type;
    public List<FileTemplate> childs;
}

