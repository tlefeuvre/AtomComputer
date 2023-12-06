using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Visualizer : MonoBehaviour
{
    public float updatesensitivity = 0.5f;
    public float minHeight = 15.0f;
    public float maxHeight = 425.0f;
    public UnityEngine.Color visualizerColor = UnityEngine.Color.green;
    [Space(15)]
    public AudioClip audioClip;
    public bool loop = true;
    [Space(15), Range(64, 8192)]
    public int visualiserSimples = 64;

    public VisualizerObjectScript[] visualizerObjects;
    AudioSource m_audioSource;
    // Start is called before the first frame update
    void Start()
    {
        visualizerObjects = GetComponentsInChildren<VisualizerObjectScript>();

        if (!audioClip)
            return;

        m_audioSource = new GameObject ("AudioSource").AddComponent<AudioSource>();
        //si besoin de faire loop le son 
        m_audioSource.loop = loop;
        m_audioSource.clip = audioClip;
        m_audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrumData = m_audioSource.GetSpectrumData(visualiserSimples, 0, FFTWindow.Rectangular);
        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;
            newSize.y = Mathf.Lerp (newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 1.5f), updatesensitivity) ;
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;
            visualizerObjects[i].GetComponent<Image> ().color = visualizerColor;
        }
    }
}
