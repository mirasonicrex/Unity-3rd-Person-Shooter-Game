using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips; //array of audio clips
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true; //boolean is always false at first so it must be set to true
    }

    // Update is called once per frame
    public void Play ()
    {
        if (!canPlay)
            return; 

        GameManager.Instance.Timer.Add(() =>
        {
            canPlay = true;
        }, delayBetweenClips); //https://freesound.org/ 

        canPlay = false;


        int clipIndex = Random.Range(0, clips.Length); //[plays a random clip
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }
}
