using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomListener : MonoBehaviour
{
    public AudioSource audioClip;
    public Vector2 randomNumber;
    bool clipPlayed=false;
    float value;

    void Start()
    {
        value = Random.Range(randomNumber.x, randomNumber.y);
    }

    void Update()
    {
        if (clipPlayed)
        {
            value = Random.Range(randomNumber.x, randomNumber.y);
            clipPlayed = false;
        }

        else
        {
            if (value > 0)
            {
                value -= Time.deltaTime;
                clipPlayed = false;
            }
            else 
            {
                clipPlayed = true;
                audioClip.Play();
            }
        }
    }
}
