using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


/**
 * @class MusicManager
 */
public class MusicManager : MonoBehaviour
{
    public bool        WebGLBuild        = false;
    public static bool WebGLBuildSupport = false;

    public float SFX_Volume  = 50;
    public float Main_Volume = 50;

    public AudioMixerGroup SFXMixer;
    public AudioMixerGroup MainMixer;

    public GameObject       soundObjet;
    public List<SoundEvent> events  = new List<SoundEvent>();
    public List<GameObject> targets = new List<GameObject>();

    private static MusicManager instance;

    /**
     * Called when the object is loaded
     */
    void Awake()
    {
        MusicManager.WebGLBuildSupport = WebGLBuild;

        if(!WebGLBuild)
        {
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            this.gameObject.AddComponent<AudioListener>();
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    /**
     * Check for terminated sounds
     */
    void Update()
    {
        for(int nObject = targets.Count - 1; nObject >= 0; --nObject)
        {
            AudioSource source = targets[nObject].GetComponent<AudioSource>();
         
            if (!source.isPlaying)
            {
                Destroy(targets[nObject]);
                targets.RemoveAt(nObject);
            }
        }
    }

    /**
     * Plays a sound
     */
    public static void PostEvent(string name)
    {
        if (MusicManager.instance == null)
        {
            return;
        }

        SoundEvent soundEvent = MusicManager.instance.GetClipFromName(name);

        if(soundEvent == null)
        {
            Debug.Log("Event not found : " + name);
            return;
        }

        // Process event type
        if(soundEvent.Play)
        {
            MusicManager.instance.PlayEvent(soundEvent);
        }
        else if(soundEvent.Stop)
        {
            MusicManager.instance.StopEvent(soundEvent);
        }
    }

    /**
     * RTPC main volume 
     */
    public static void SetMainVolume(float timescale)
    {
        AudioMixerGroup mixer = MusicManager.instance.MainMixer;

        if(timescale != 1.0f)
        {
            mixer.audioMixer.SetFloat("MainVol", -5.0f);
        }
        else
        {
            mixer.audioMixer.SetFloat("MainVol", 3.0f);
        }
    }

    /**
     * Start an event
     */
    private void PlayEvent(SoundEvent soundEvent)
    {
        if(GetTargetInstanceCount(soundEvent.SoundEventTarget[0].name) >= soundEvent.SoundEventMaxInstance)
        {
            return;
        }

        GameObject  go     = Instantiate(soundObjet, this.transform);
        AudioSource source = go.GetComponent<AudioSource>();

        // Setting up the name
        go.name = soundEvent.SoundEventTarget[0].name;

        // Chosing the right mixer
        if(soundEvent.SoundEventType == SoundEvent.Type.SFX)
        {
            source.outputAudioMixerGroup = SFXMixer;
        }
        else
        {
            source.outputAudioMixerGroup = MainMixer;
        }
            
        source.loop           = soundEvent.SoundEventLoop;
        source.clip           = soundEvent.SoundEventTarget[Random.Range(0, soundEvent.SoundEventTarget.Count)];
        source.volume         = soundEvent.SoundEventVolume;
        source.pitch          = soundEvent.SoundEventPitch;
        source.reverbZoneMix  = soundEvent.SoundEventReverb;

        if(soundEvent.Reverb)
        {
            AudioReverbFilter filter = go.AddComponent<AudioReverbFilter>();
            filter.reverbPreset = soundEvent.SoundEventReverbPreset;
        }

        if(soundEvent.RandomizePitch)
        {
            source.pitch += Random.Range(soundEvent.PitchRandomRange.x, soundEvent.PitchRandomRange.y);
        }
        
        if(soundEvent.RandomizeVolume)
        {
            source.volume += Random.Range(soundEvent.VolumeRandomRange.x, soundEvent.VolumeRandomRange.y);
        }

        source.Play();
        targets.Add(go);
    }

    /**
     * Removes all occurences of the target event
     */
    private void StopEvent(SoundEvent soundEvent)
    {
        for (int nObject = targets.Count - 1; nObject >= 0; --nObject)
        {
            AudioSource source = targets[nObject].GetComponent<AudioSource>();

            if (targets[nObject].name == soundEvent.SoundEventTarget[0].name)
            {
                source.Stop();
                Destroy(targets[nObject]);
                targets.RemoveAt(nObject);
            }
        }
    }

    /**
     * Gets back the sound event from the event name
     */
    private SoundEvent GetClipFromName(string name)
    {
        foreach(SoundEvent soundEvent in events)
        {
            if(soundEvent.SoundEventName == name)
            {
                return soundEvent;
            }
        }
        
        return null;
    }

    /**
     * Returns the number of playing target event
     */
    private int GetTargetInstanceCount(string name)
    {
        int count = 0;
        foreach (GameObject go in targets)
        {
            if (go.name == name)
            {
                count += 1;
            }
        }

        return count;
    }
}
