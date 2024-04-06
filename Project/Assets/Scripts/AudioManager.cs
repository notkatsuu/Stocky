using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider volume;
    [SerializeField] private AudioSource audioSource;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeVolume()
    {
        audioSource.volume = volume.value;
    }
}
