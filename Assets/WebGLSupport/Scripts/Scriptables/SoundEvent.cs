#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores informations about a sound event
 * @class SoundEvent
 */
[CreateAssetMenu(fileName = "SoundEvent", menuName = "Shmup/SoundEvent")]
public class SoundEvent : ScriptableObject
{
    [SerializeField]
    public enum Type
    {
        SFX   = 0,
        Music = 1
    }

    [HideInInspector] [SerializeField] public bool      Play;
    [HideInInspector] [SerializeField] public bool      Stop;
    [HideInInspector] [SerializeField] public bool      Reverb;
    [HideInInspector] [SerializeField] public string    SoundEventName;

    [SerializeField] public List<AudioClip> SoundEventTarget = new List<AudioClip>();

    [HideInInspector] [SerializeField] public Type      SoundEventType;
    [HideInInspector] [SerializeField] public bool      SoundEventLoop;
    [HideInInspector] [SerializeField] public int       SoundEventMaxInstance;
    [HideInInspector] [SerializeField] public float     SoundEventVolume;
    [HideInInspector] [SerializeField] public float     SoundEventPitch;
    [HideInInspector] [SerializeField] public float     SoundEventReverb;
 
    [HideInInspector] [SerializeField] public bool              RandomizeVolume;
    [HideInInspector] [SerializeField] public bool              RandomizePitch;
    [HideInInspector] [SerializeField] public Vector2           PitchRandomRange;
    [HideInInspector] [SerializeField] public Vector2           VolumeRandomRange;
    [HideInInspector] [SerializeField] public AudioReverbPreset SoundEventReverbPreset;

    /**
     * Avoid object reset when the scene change or after play/stop
     */
    private void OnEnable()
    {
        SoundEventName = this.name;
    }

    /**
     * Avoid object reset when the scene change or after play/stop
     */
    private void OnDisable()
    {
        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
        #endif
    }
}