using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public float FillSpeed = 0.5f;
    private float targetProgress = 0;

    public float timeToFill;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        IncrementProgress(1f);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (slider.value < targetProgress && timeToFill >0)
            slider.value +=  Time.deltaTime/ timeToFill;
        
    }

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
