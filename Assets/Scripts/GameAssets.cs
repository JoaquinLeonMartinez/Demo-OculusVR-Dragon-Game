using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private GameAssets()
    {

    }
    private static GameAssets instance;

    public static GameAssets Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("No existe ninguna instancia para gameAssets");
                //instance = new GameAssets();
                instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return instance;
        }
    }


    
    public SoundAudioClip[] soundAudioCplipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
