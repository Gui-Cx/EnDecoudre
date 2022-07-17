using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    public static SoundAssets instance;

    public SoundAudioClip[] soundAudioClipsArray;

    private float stepTimer;
    public bool canPlayStep = true;

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
            Debug.Log("Il y a déjà une instance de SoundManager : Autodestruction lancée ");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayPlayerDiceSound(int player)
    {
        int soundToPlay = Random.Range(0, 3);
        if (player == 0)
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
        if (player == 0)
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
