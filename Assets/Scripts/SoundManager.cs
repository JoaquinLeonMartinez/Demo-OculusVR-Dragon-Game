using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    
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

}
