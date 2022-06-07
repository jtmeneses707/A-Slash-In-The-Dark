using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    private float musicVolume;

    private EventInstance musicEvent;

    /* SongState:
     * 0 = loop calm
     * 1 = stop looping calm, letting it transition to silence
     * 2 = lightning  */
    public int songState { set { musicEvent.setParameterByName("SongState", value); } }

    public float lowPassAmount { 
        get { return _lowPassAmount;  }
        set
        {
            _lowPassAmount = value;
            musicEvent.setParameterByName("LowPassAmount", _lowPassAmount);
        }
    }
    float _lowPassAmount;

    public bool shouldIncrementLowpassAmount = false;

    void Start()
    {
        musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/music");
        musicEvent.setVolume(musicVolume);
    }

    void OnDestroy()
    {
        StopMusic();
    }

    public void PlayMusic()
    {
        musicEvent.start();
    }

    public void StopMusic()
    {
        musicEvent.stop(STOP_MODE.IMMEDIATE);
    }

    public IEnumerator IncrementLowPassCoroutine(float delay, float duration)
    {
        yield return new WaitForSeconds(delay);
        float startTime = Time.time;
        float incrementAmount = Time.deltaTime / (duration);
        while(Time.time < startTime + duration)
        {
            if(!shouldIncrementLowpassAmount)
            {
                break;
            }
            Debug.Log(lowPassAmount);
            float newLowPassAmount = lowPassAmount + incrementAmount;
            lowPassAmount = Mathf.Clamp01(newLowPassAmount);
            yield return null;
        }
        lowPassAmount = 1;
    }

}
