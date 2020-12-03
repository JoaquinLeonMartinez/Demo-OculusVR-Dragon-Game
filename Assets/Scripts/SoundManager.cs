using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager Instance;

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
        //Play("Musica de fondo")
    }

    private void Update()
    {

    }

    private void RandomizePitch()
    {
        foreach (Sound s in sounds)
        {
            s.source.pitch = s.pitch * UnityEngine.Random.Range(0.5f, 1.5f);
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
