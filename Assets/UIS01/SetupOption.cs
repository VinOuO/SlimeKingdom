using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupOption : MonoBehaviour {

    public Slider[] volumeSliders;
    public Toggle[] screenSizeToggles;
    public int[] screenWidths;
    int activeScreenSizeIndex;
    public float abc;
    public int IsMute;
    public Toggle checkmute;

    private void Start()
    {
        abc = 1;
        activeScreenSizeIndex = 1;
        bool isFullScreen = false;
        /*if (checkmute.isOn)
        {
            IsMute = 1;
        }
        else
            IsMute = 0;*/
        IsMute = AudioManager.instance.masterVolumeMute;
        if(IsMute == 1)
        checkmute.isOn = true;
        else
            checkmute.isOn = false;


        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;

    }

    public void Play()
    {

    }

    public void SetScreenSize(int i)
    {
        if(screenSizeToggles [i].isOn)
        {
            activeScreenSizeIndex = i;
            float aspectRaio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRaio) , false);
            
        }
    }

    public void FullScreen(bool isFullscreen)
    {
        for(int i = 0; i < screenSizeToggles.Length; i++)
        {
            screenSizeToggles [i].interactable = !isFullscreen;
        }

        if(isFullscreen)
        {
            Resolution[] allResloution = Screen.resolutions;
            Resolution maxResloution = allResloution[allResloution.Length - 1];
            Screen.SetResolution(maxResloution.width, maxResloution.height, true);
        }
        else
        {
            SetScreenSize(activeScreenSizeIndex);
        }
    }

    public void SetMusicVolumeMute()
    {
        if (checkmute.isOn)
        {
            IsMute = 1;
        }
        else
            IsMute = 0;
        AudioManager.instance.SetVolumeMute(IsMute);
        AudioManager.instance.SetVolume(abc*IsMute, AudioManager.AudioChannel.Master);

    }

    public void SetMusicVolume(float value)
    {
        abc = volumeSliders[0].value;
        AudioManager.instance.SetVolume(abc*IsMute, AudioManager.AudioChannel.Master);

    }

}
