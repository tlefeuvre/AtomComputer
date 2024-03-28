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
    public GameObject FalconActivationPrefab;


    [Header("Icones")]
 
    public Sprite folderImage;
    public Sprite textFileImage;
    public Sprite audioFileImage;
    public Sprite lockFileImage;
    public Sprite successFileImage;
    public Sprite ImageFileImage;
    public Sprite hiddenFileImage;

    private Dictionary<FileType, Sprite> filesImagesDictionnary;

    [Header("Audio")]
    public AudioClip clickAudio;
    public AudioClip onStartAudio;
    public AudioClip successClickAudio;
    public AudioClip errorClickAudio;
    public AudioClip loadingAudio;
    private AudioSource audioSource;

    [Header("Others")]
    public GameObject backButton;
    public GameObject depthPrefab;
    public GameObject unlockPrefab;
    public GameObject tutoPrefab;
    public Architecture architecture;
    public List<FileTemplate> arborescence = new List<FileTemplate>();

    public List<GameObject> hiddenFiles = new List<GameObject>();

    private float scaleFile = 1.8f;
    private bool allUnlocked;
    public bool isDisplay = false;
    public float YOffset = 70;

    public bool firstTuto = false;
    void Start()
    {
        //Screen.SetResolution(1152, 864, false);

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
            { FileType.HIDDEN,hiddenFileImage},
        };

        instance = this;
        NewWindow();

        audioSource.clip = onStartAudio;
        audioSource.Play();
    }
    private void Update()
    {
        transform.SetAsFirstSibling();
        if (isDisplay)
        {
            Debug.Log("not hide hupdate");

            foreach (GameObject file in hiddenFiles)
                if (file)
                    file.SetActive(true);
        }

        else
        {
            Debug.Log("hide hupdate");
            foreach (GameObject file2 in hiddenFiles)
                if (file2)
                    file2.SetActive(false);
        }
    }
    public void NewWindow()
    {
        PlayClickAudio();
        Debug.Log("new window");
        /* Clear */
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        depthPrefab.GetComponentInChildren<TMP_Text>().text ="Level "+'\n'+ arborescence[arborescence.Count - 1].depthIndex.ToString();
        unlockPrefab.GetComponentInChildren<TMP_Text>().text ="Folder "+'\n'+ (arborescence[arborescence.Count - 1].depthIndex-1).ToString() +" unlocked";
        GetComponent<GridLayoutGroup>().enabled = true;
        StartCoroutine("desactivatepopup");

        /* Instantiates file childs */
        int id = arborescence.Count - 1;



        if (arborescence[id].type == FileType.FOLDER || arborescence[id].type == FileType.LOCK || arborescence[id].type == FileType.HIDDEN)
        {
            //backButton.SetActive(true);
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

            OpenImageFile(arborescence[id].imageContent, arborescence[id].textFileName, arborescence[id].idToSend, arborescence[id].sendable);
        }
    }

    /* Create new files (folder, text...) when opening a folder */
    private void NewFile(Sprite image, int index, string name, FileType fileType, string password)
    {
        GameObject newFile = Instantiate(filePrefab);
        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one * scaleFile;
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);
        newFile.GetComponent<FileButtonManager>().SetParameters(image,index, name, fileType, password);
        if(fileType == FileType.HIDDEN)
        {
            hiddenFiles.Add(newFile);
            newFile.SetActive(false);
        }
        
    }

    /* Display text content of a text file*/
    private void OpenTextFile(string fileName, string textContent, int id, bool sendable)
    {
        GetComponent<GridLayoutGroup>().enabled = false;

        GameObject newFile = Instantiate(textContentPrefab);

        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one;

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, YOffset);
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

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, YOffset);
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);

        float clipLenght = audioClip.length;
        
        newFile.GetComponent<AudioFileManager>().SetParameters(fileName, clipLenght, audioClip,id,sendable);

        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void OpenImageFile(Sprite img, string name, int id, bool sendable)
    {
        GetComponent<GridLayoutGroup>().enabled = false;

        GameObject newFile = Instantiate(ImagePrefab);

        newFile.transform.SetParent(transform);
        newFile.transform.localScale = Vector3.one ;

        newFile.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1 * transform.GetComponent<RectTransform>().anchoredPosition.x, YOffset);
        newFile.transform.localPosition = new Vector3(newFile.transform.localPosition.x, newFile.transform.localPosition.y, 0);

        newFile.GetComponent<ImageFileManager>().SetParameters(img,name, id, sendable);

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

    IEnumerator desactivatepopup()
    {
        yield return new WaitForSeconds(1.0f);
        unlockPrefab.SetActive(false);
    }

    public void DisplayHiddenFiles()
    {
        if (!firstTuto)
        {
            tutoPrefab.SetActive(true);
            firstTuto = true;
        }
        isDisplay = true;
    }
    public void HideHiddenFiles()
    {
        isDisplay = false;
    }
    public void ActivateFalcon()
    {
        HideHiddenFiles();
        FalconState.Instance.SetFalconState(true);
        FalconActivationPrefab.SetActive(true );
    }

    public void PlayClickAudio()
    {
        if (audioSource)
        {
            audioSource.clip = clickAudio;
            audioSource.Play();
        }
    }

    public void PlayErrorAudio()
    {
        if (audioSource)
        {
            audioSource.clip = clickAudio;
            audioSource.Play();
        }
    }
    public void PlaySuccessAudio()
    {
        if (audioSource)
        {
            audioSource.clip = clickAudio;
            audioSource.Play();
        }
    }
    public void PlayLoadingAudio()
    {
        if (audioSource)
        {
            audioSource.clip = clickAudio;
            audioSource.Play();
        }
    }
}
