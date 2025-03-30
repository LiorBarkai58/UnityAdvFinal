using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    

    public void HandlePlayerDied()
    {
        mixer.SetFloat("BgmDistortion", 1f);
        mixer.SetFloat("BgmPitch", .80f);
        mixer.SetFloat("BgmVolume", -50f);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void ResetMixer()
    {
        mixer.SetFloat("BgmDistortion", 0.5f);
        mixer.SetFloat("BgmPitch", 1f);
        mixer.SetFloat("BgmVolume", -0.19f);
    }

}

