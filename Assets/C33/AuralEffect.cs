using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AuralEffect : MonoBehaviour {

    bool activated;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (activated && !source.isPlaying)
            Destroy(this.gameObject);
    }

    public void Play(Vector3 position, SoundInfo sInfo)
    {
        activated = true;
        transform.position = position;
        source.clip = sInfo.clip;
        source.volume = sInfo.volume;
        source.Play();
    }

}
