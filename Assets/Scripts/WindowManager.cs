using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    public static WindowManager instance;

    [Header("Prefabs")]
    public GameObject filePrefab;
    public GameObject textContentPrefab;
    public GameObject audioContentPrefab;
    public GameObject VictoryPrefab;
    public GameObject ImagePrefab;


    [Header("Icones")]
 
    public Sprite folderImage;
    public Sprite textFileImage;
    public Sprite audioFileImage;
    public Sprite lockFileImage;
    public Sprite successFileImage;
    public Sprite ImageFileImage;

    private Dictionary<FileType, Sprite> filesImagesDictionnary;

    [Header("Others")]
    public AudioClip clickAudio;
    public AudioClip onStartAudio;
    public GameObject backButton;
    public GameObject depthPrefab;
    public Architecture architecture;
    public List<FileTemplate> arborescence = new List<FileTemplate>();

    private AudioSource audioSource;

    private bool allUnlocked;
    void Start()
    {
        allUnlocked = false;
        arborescence.Add(architecture.filesList[0]);
        audioSource = GetComponent<AudioSource>();

        filesImagesDictionnary = new Dictionary<FileType, Sprite>() {
            { FileType.FOLDER,folderImage},
            { FileType.TEXT,textFileImage},
            { FileType.AUDIO,audioFileImage},
            { FileType.LOCK,lockFileImage},
            { FileType.SUCCESS,successFileImage},
            { FileType.IMAGE,ImageFileImage},
        };

        instance = this;
        NewWindow();

        audioSource.clip = onStartAudio;
        audioSource.Play();
    }

    public void NewWindow()
    {
        audioSource.clip = clickAudio;
        audioSource.Play();
        Debug.Log("new window");
        /* Clear */
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        depthPrefab.GetComponentInChildren<TMP_Text>().text ="Niveau"+'\n'+ arborescence[arborescence.Count - 1].depthIndex.ToString();
        GetComponent<GridLayoutGroup>().enabled = true;

        /* Instantiates file childs */
        int id = arborescence.Count - 1;



        if (arborescence[id].type == FileType.FOLDER || arborescence[id].type == FileType.LOCK)
        {
            backButton.SetActive(true);
            depthPrefab.SetActive(true );
            for (int i = 0; i < arborescence[id].childs.Count; i++)
            {
                if (allUnlocked && arborescence[id].childs[i].isLock && arborescence[id].childs[i].type == FileType.LOCK)
                    arborescence[id].childs[i].type = FileType.FOLDER;
                else if(!allUnlocked && arborescence[id].childs[i].isLock && arborescence[id].childs[i].type == FileType.FOLDER)
                    arborescence[id].childs[i].type = FileType.LOCK;


                NewFile(filesImagesDictionnary[arborescence[id].childs[i].type], i, arborescence[id].childs[i].name, arborescence[id].childs[i].type, arborescence[id].childs[i].password);
            }
        }

        else if(arborescence[id].type == FileType.TEXT)
        {
            backButton.SetActive(false);
            depthPrefab.SetActive(false);

            OpenTextFile(arborescence[id].textFileName, arborescence[id].textContent, arborescence[id].idToSend, arborescence[id].sendable);
        }

        else if (arborescence[id].type == FileType.AUDIO)
        {
            backButton.SetActive(false);
            depthPrefab.SetActive(false);

            OpenAudioFile(arborescence[id].audioContent, arborescence[id].textFileName, arborescence[id].idToSend, arborescence[id].sendable);
        }
        else if (arborescence[id].type == FileType.IMAGE)
        {
            backButton.SetActive(false);
            depthPrefab.SetActive(false);

            OpenImageFile(arborescence[id].imageContent, arborescence[id].textFileName);
        }
    }

    /* Create new files (folder, text...) when opening a folder */
    private void NewFile(Sprite image, int index, string name, FileType fileType, string password)
    {
        GameObject newFile = Instantiate(filePrefab);
        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one;
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);
        newFile.GetComponent<FileButtonManager>().SetParameters(image,index, name, fileType, password);
    }

    /* Display text content of a text file*/
    private void OpenTextFile(string fileName, string textContent, int id, bool sendable)
    {
        GetComponent<GridLayoutGroup>().enabled = false;

        GameObject newFile = Instantiate(textContentPrefab);

        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one;

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, 0);
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);

        newFile.GetComponent<TextFileManager>().SetParameters(fileName, textContent,id,sendable);
        //newFile.GetComponentInChildren<TMP_Text>().text = textContent;
    }

    /* Play audio clip of audio file */
    private void OpenAudioFile(AudioClip audioClip, string fileName, int id, bool sendable)
    {
        GetComponent<GridLayoutGroup>().enabled = false;

        GameObject newFile = Instantiate(audioContentPrefab);

        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one;

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, 0);
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);

        float clipLenght = audioClip.length;
        
        newFile.GetComponent<AudioFileManager>().SetParameters(fileName, clipLenght, audioClip,id,sendable);


        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void OpenImageFile(Sprite img, string name)
    {
        GetComponent<GridLayoutGroup>().enabled = false;

        GameObject newFile = Instantiate(ImagePrefab);

        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one;

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, 0);
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);

        newFile.GetComponent<ImageFileManager>().SetParameters(img,name);

    }
    /* Go back in arborescence */
    public void GoBack()
    {
        if (arborescence.Count - 1 <= 0)
            return;

        audioSource.Stop();
        arborescence.RemoveAt(arborescence.Count - 1);
        NewWindow();
    }

    public void Victory()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("vitory");
        GetComponent<GridLayoutGroup>().enabled = false;

        GameObject newFile = Instantiate(VictoryPrefab);
        Debug.Log(newFile.name);

        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one;

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, 0);
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);
    }
}
