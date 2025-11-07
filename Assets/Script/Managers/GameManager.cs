using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ← Sobrevive al cambio de escena
    }

    public AudioSource audioSource;
    
    public void OneShootAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
