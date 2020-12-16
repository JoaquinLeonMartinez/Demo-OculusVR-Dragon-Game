using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager Instance;

    public FootSteps footSteps;

    //public List<AudioSource> enemiesAudioOld;

    public List<AudioClip> enemiesAudio;

    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    private void Update()
    {

    }
    /*
    public float InvokeEnemiesAudios()
    {
        if (enemiesAudio != null)
        {
            int randomNumb = UnityEngine.Random.Range(0, enemiesAudio.Count);
            if (enemiesAudio[randomNumb] != null)
            {
                enemiesAudio[randomNumb].Play();
                float duration = enemiesAudio[randomNumb].clip.length;
                enemiesAudio.RemoveAt(randomNumb); //asi no se repetiran audios
                return duration;
            }
        }
        return 0;
    }
    */
    public float InvokeEnemiesAudios()
    {
        if (enemiesAudio != null)
        {
            int randomNumb = UnityEngine.Random.Range(0, enemiesAudio.Count);
            if (enemiesAudio[randomNumb] != null)
            {
                //Debug.Log($"Vamos a reproducir el audio {enemiesAudio[randomNumb]}");
                audioSource.clip = enemiesAudio[randomNumb];
                audioSource.Play();
                //Debug.Log($"Ya se esta reproduciendo el audio {audioSource.clip.name}");
                float duration = audioSource.clip.length;
                enemiesAudio.RemoveAt(randomNumb); //asi no se repetiran audios
                return duration;
            }
        }
        else
        {
            Debug.LogError("No se encuentra la lista de audios de los enemigos");
        }
        return 0;
    }

    private void RandomizePitch()
    {
        foreach (Sound s in sounds)
        {
            if (s.name != "Theme" && s.name != "Explication_1"  && s.name != "Explication_2" && s.name != "Explication_3" && s.name != "Explication_1_second" && s.name != "Roar")
            {
                s.source.pitch = s.pitch * UnityEngine.Random.Range(0.5f, 1.5f);
            }
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            RandomizePitch();
            s.source.Play();
        }
        else
        {
            Debug.LogError($"No se ha encontrado el sonido {name}");
            return;
        }
        
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.volume = volume;
        }
        else
        {
            Debug.LogError($"No se ha encontrado el sonido {name}");
            return;
        }

    }

    public float GetLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            return s.clip.length;
        }
        else
        {
            Debug.LogError($"No se ha encontrado el sonido {name}");
            return 0f;
        }
    }

    /*
    public enum Sound
    {
        DragonWings,
        Roar,
        Fire,
        Explosion,
        Steps

    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {

        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioCplipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError($"No se ha encontrado el sonido especificado: {sound}");
        return null;
    }
    */

}
