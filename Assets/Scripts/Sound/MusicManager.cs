using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public float musicVolume;

    FMOD.Studio.EventInstance musicEvent;

    void Start()
    {
        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/music");
        musicEvent.setVolume(musicVolume);
        musicEvent.start();
    }

}
