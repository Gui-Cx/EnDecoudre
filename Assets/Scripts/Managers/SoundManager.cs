using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        None,
        PlayerArrival,
        DiceRoll,
        CLangMetal,
        DieDave1,
        DieDave2,
        DieDave3,
        DieDerminator1,
        DieDerminator2,
        OpenMetallicDoor,
        OuchDave,
        OuchDerminator1,
        OuchDerminator2
    }

    public static void PlaySound(Sound sound, float volume = 1f)
    {
        if (sound == Sound.None) return;

        GameObject soundGameObject = new GameObject("Sound");

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(soundGameObject, audioSource.clip.length);
    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        if (sound == Sound.None) return null;

        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }

    public static float GetAudioClipLength(Sound sound)
    {
        if (sound == Sound.None) return 0f;

        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.instance.soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip.length;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return 0f;
    }
}
