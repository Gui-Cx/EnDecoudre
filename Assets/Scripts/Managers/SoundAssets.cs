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

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.volume = mainMusicVolume * musicVolumeModifier;
        musicSource.Play();
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
                    SoundManager.PlaySound(SoundManager.Sound.YeetDave1);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.YeetDave2);
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
                    SoundManager.PlaySound(SoundManager.Sound.YeetDerminator1);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.YeetDerminator2);
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
                    SoundManager.PlaySound(SoundManager.Sound.DieDave1);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.DieDave2);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.DieDave3);
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
                    SoundManager.PlaySound(SoundManager.Sound.DieDerminator1);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.DieDerminator2);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.DieDerminator2);
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
                SoundManager.PlaySound(SoundManager.Sound.Sword1);
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.Sword2);
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
                SoundManager.PlaySound(SoundManager.Sound.Piou1);
                break;
            case 1:
                SoundManager.PlaySound(SoundManager.Sound.Piou2);
                break;
            case 2:
                SoundManager.PlaySound(SoundManager.Sound.Piou1);
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
                    SoundManager.PlaySound(SoundManager.Sound.OuchDave);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDave);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDave);
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
                    SoundManager.PlaySound(SoundManager.Sound.OuchDerminator1);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDerminator2);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.OuchDerminator2);
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayOpenDoor()
    {
        SoundManager.PlaySound(SoundManager.Sound.OpenMetallicDoor);
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
