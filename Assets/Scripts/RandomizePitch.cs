using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomizePitch : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Randomize()
    {
        _audioSource.pitch = Random.Range(0.6f, 1.6f);
    }
}
