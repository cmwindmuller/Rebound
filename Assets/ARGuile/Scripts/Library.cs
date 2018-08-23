using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct SoundInfo
{
    public string key;
    public AudioClip clip;
    public float volume;
}

public class Library : MonoBehaviour {

    public static Library main;

    public AuralEffect soundFX;
    public SoundInfo[] sounds;
    IDictionary<string, SoundInfo> soundDictionary;
    
	void Awake () {
        main = this;
        soundDictionary = new Dictionary<string, SoundInfo>();
        foreach(SoundInfo sInfo in sounds)
        {
            soundDictionary.Add(sInfo.key, sInfo);
        }
	}

    public static void playSound(Vector3 position, string key)
    {
        if (!main.soundDictionary.ContainsKey(key))
            return;
        try
        {
            AuralEffect afx = Instantiate(main.soundFX);
            afx.Play(position, main.soundDictionary[key]);
        }
        catch(NullReferenceException e)
        {
            Debug.Log(key);
        }
    }

}
