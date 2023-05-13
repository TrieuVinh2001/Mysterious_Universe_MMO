using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumeSetting : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject soundpanel;
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            //Lấy giá trị của các slider để gán cho mixer
            mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));

            SetSlider();
        }
        else
        {
            SetSlider();
        }
    }

    public void Volume()
    {
        AudioManager.instance.PlaySFX(3);
        soundpanel.SetActive(true);//Hiện panel
    }

    private void SetSlider()
    {
        masterVolume.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void UpdateMasterVolume()//Được gọi trong slider mỗi khi thay đổi giá trị
    {
        mixer.SetFloat("MasterVolume", masterVolume.value);//thay đổi giá trị trong mixer
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);//thay đổi giá trị của key MasterVolume
    }

    public void UpdateMusicVolume()
    {
        mixer.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
    }
    public void UpdateSFXVolume()
    {
        mixer.SetFloat("SFXVolume", sfxVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
    }
}
