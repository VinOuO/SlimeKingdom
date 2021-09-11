using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioChannel { Master, Sfx, Music };

    public float masterVolumePercent { get; private set; }
    public int masterVolumeMute { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }



    AudioSource[] musicSources;

    int activeMusicSourceIndex;

    public static AudioManager instance;

    SoundLibary libary;
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
             DontDestroyOnLoad(gameObject);
             instance = this;
               libary = GetComponent<SoundLibary>();
               musicSources = new AudioSource[2];
               for(int i = 0; i <2; i++)
               {
                    GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
                    musicSources[i] =  newMusicSource.AddComponent<AudioSource>();
                    newMusicSource.transform.parent = transform;
               }
               masterVolumePercent = 1;
               sfxVolumePercent = 1;
               musicVolumePercent = 1;
               masterVolumeMute = 1;
        }  
       



    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].loop = true;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));

    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }      
    }

    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(libary.GetClipFromName(soundName), pos);
    }

    
    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;
        while(percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
            yield return null;
        }

    }
    public void SetVolumeMute(int input)
    {
        masterVolumeMute = input;
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        if(musicSources[0])
        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        if (musicSources[1])
            musicSources[1].volume = musicVolumePercent * masterVolumePercent;

    }

}
