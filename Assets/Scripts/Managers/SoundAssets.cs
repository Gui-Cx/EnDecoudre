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
            Debug.Log("Il y a d�j� une instance de SoundManager : Autodestruction lanc�e ");
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
                default:
                    break;
            }
        }
    }

    public void PlayFootstep(bool eating)
    {
        if (canPlayStep)
        {
            canPlayStep = false;
            stepTimer = stepFrequency;
            float vol = 0.5f;
            if (eating)
            {
                stepTimer = stepFrequency * 1.5f;
            }
            stepTimer = Random.Range(stepTimer / 2, stepTimer);
            int step = Random.Range(0, 3);

            switch (step)
            {
                case 0:
                    SoundManager.PlaySound(SoundManager.Sound.PlayerArrival, vol);
                    break;
                case 1:
                    SoundManager.PlaySound(SoundManager.Sound.PlayerArrival, vol);
                    break;
                case 2:
                    SoundManager.PlaySound(SoundManager.Sound.PlayerArrival, vol);
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
