using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public bool is2D;
	
    public void Play(int num, float vol)
    {
        GameObject obj = new GameObject();
        obj.transform.position = transform.position;
        obj.name = "AUDIO_" + clips[num].name;

        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clips[num];
        source.volume = vol;
        //source.pitch = pit; //Not necessary but can be optional.
        if (!is2D) source.spatialBlend = 1;
        else source.spatialBlend = 0;
        source.Play();

        Destroy(obj, clips[num].length); //Don't mistake length(length of clip) with "L"ength
    }
}
