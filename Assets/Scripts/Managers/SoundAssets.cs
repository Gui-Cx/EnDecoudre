using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets instance;

    public SoundAudioClip[] soundAudioClipsArray;

    private float stepTimer;
    public bool canPlayStep = true;
    public AudioClip mainMusic;
    public AudioClip ambiantMusic;
    private AudioSource musicSource;

    public AudioClip StartMusic;
    public AudioClip EndMusic;

    public float musicVolumeModifier = 0.35f;
    public float sfxVolumeModifier = 0.5f;

    public float mainMusicVolume = 1f;
    public float mainSFXVolume = 1f;

    public Slider musicSlide;
    public Slider ambiantSlide;
    public Slider sfxSlide;

    [SerializeField] private float stepFrequency = 0.3f;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

    public void Awake()
    {
        if (instance)
        {
            Debug.Log("Il y a d�j� une instance de SoundManager : Autodestruction lanc�e ");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        musicSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 0.5f; 
        mainMusicVolume = 0.5f;
        mainSFXVolume = 0.5f;
    }
    internal void PlayStartMusic()
    {
        StartCoroutine(UpdateMusicWithFade(musicSource, StartMusic,0.5f));  
    }

    internal void PlayMainMusic()
    {
        StartCoroutine(UpdateMusicWithFade(musicSource,mainMusic,0.5f));
    }

    internal void PlayGameOverMusic()
    {
        StartCoroutine(UpdateMusicWithFade(musicSource, EndMusic, 0.5f));
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.volume = mainMusicVolume * musicVolumeModifier;
        musicSource.Play();
    }

    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
            activeSource.Play();

        float t = 0.0f;

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime)) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }
        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }
    }
    public IEnumerator StopMusicWithFade(float transitionTime = 1.0f)
    {

        float t = 0f;

        for (t = 0f; t <= transitionTime; t += Time.deltaTime)
        {
            musicSource.volume = (1 - (t / transitionTime)) * mainMusicVolume * musicVolumeModifier;
            yield return null;
        }

        musicSource.Stop();
    }

    public void StopMusic()
    {
        StartCoroutine(StopMusicWithFade(1f));
    }

    public void PlaySpawnSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerArrival);

    }

    public void PlayYeetSound(int player)
    {
        int soundToPlay = Random.Range(0, 2);
        if (player == 1)
        {
            switch (soundToPlay)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.YeetDave1, mainSFXVolume);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.YeetDave2, mainSFXVolume);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (soundToPlay)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.YeetDerminator1, mainSFXVolume);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.YeetDerminator2, mainSFXVolume);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayBoomerang()
    {
        SoundManager.PlaySound(SoundManager.Sound.WooshBoomerang);
    }

    public void PlayPlayerDieSound(int player)
    {
        int soundToPlay = Random.Range(0, 3);
        if (player == 1)
        {            
            switch (soundToPlay)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.DieDave1, mainSFXVolume);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.DieDave2, mainSFXVolume);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.DieDave3, mainSFXVolume);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (soundToPlay)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.DieDerminator1, mainSFXVolume);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.DieDerminator2, mainSFXVolume);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.DieDerminator2, mainSFXVolume);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlaySword()
    {
        int soundToPlay = Random.Range(0, 2);
            
        switch (soundToPlay)
        {
            case 0:
                SoundManager.PlaySound(SoundManager.Sound.Sword1, mainSFXVolume);
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.Sword2, mainSFXVolume);
                break;
            default:
                break;
        }
    }

    public void PlayBullet()
    {
        int soundToPlay = Random.Range(0, 3);

        switch (soundToPlay)
        {
            case 0:
                SoundManager.PlaySound(SoundManager.Sound.Piou1, mainSFXVolume);
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.Piou2, mainSFXVolume);
                break;
            case 2:
                SoundManager.PlaySound(SoundManager.Sound.Piou1, mainSFXVolume);
                break;
            default:
                break;
        }
    }

    public void PlayTakeDamagePlayer(int player)
    {
        int soundToPlay = Random.Range(0, 3);
        if (player == 1)
        {
            switch (soundToPlay)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDave, mainSFXVolume);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDave, mainSFXVolume);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDave, mainSFXVolume);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (soundToPlay)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDerminator1, mainSFXVolume);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDerminator2, mainSFXVolume);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDerminator2, mainSFXVolume);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayOpenDoor()
    {
        SoundManager.PlaySound(SoundManager.Sound.OpenMetallicDoor, mainSFXVolume);
    }

    public void PlayFootstep()
    {
        if (canPlayStep)
        {
            canPlayStep = false;
            stepTimer = stepFrequency;
            float vol = 0.5f;
            //if (eating)
            //{
            //    stepTimer = stepFrequency * 1.5f;
            //}
            stepTimer = Random.Range(stepTimer / 2, stepTimer);
            int step = Random.Range(0, 3);

            switch (step)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.Walk1, vol);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.Walk1, vol);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.Walk1, vol);
                    break;
                default:
                    break;
            }
        }

    }
    public void Update()
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0)
        {
            canPlayStep = true;
        }
    }
}
