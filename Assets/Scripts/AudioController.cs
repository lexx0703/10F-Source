using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    float volume = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_AudioSource.volume = volume;
    }
    public void SetVolume(float vol)
    {
        volume = vol;
    }
}
