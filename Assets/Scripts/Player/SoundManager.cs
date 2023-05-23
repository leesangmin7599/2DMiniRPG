using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audiomixer;
    public AudioSource bgmsound;
    public AudioClip[] bgmclip;
    public static SoundManager instance;
    public GameObject Fadeinout;
    GameObject player;
    public bool istrue = true;
    public bool istrue1 = true;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i<bgmclip.Length; i++)
        {
            if(arg0.name == bgmclip[i].name)
            {
                BgmSoundPlayer(bgmclip[i]); // 위에 둘다 호출되지않는다면 새로운 배경플레이 생성
                if (arg0.name == "TutorialMap")
                {
                    return;
                }
                if (arg0.name == "City" && istrue)
                {
                    return;
                }
            }
        }
    }
    IEnumerator LateSound()
    {
        yield return new WaitForSeconds(1f);
        istrue1 = false;
    }
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject audiosound = new GameObject(sfxName + "Sound");
        AudioSource audiosource = audiosound.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = audiomixer.FindMatchingGroups("SFX")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(audiosound, clip.length);
    }
    public void SFXPlay2(string sfxName, AudioClip clip)
    {
        GameObject audiosound = new GameObject(sfxName + "Sound");
        AudioSource audiosource = audiosound.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = audiomixer.FindMatchingGroups("SFX2")[0];
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(audiosound, clip.length);
    }

    public void BgmSoundPlayer(AudioClip clip)
    {
        bgmsound.outputAudioMixerGroup = audiomixer.FindMatchingGroups("BgmSound")[0];
        bgmsound.clip = clip;
        bgmsound.loop = true;
        bgmsound.volume = 0.1f;
        bgmsound.Play();
    }

    public void BgmSoundVolume(float val)
    {
        audiomixer.SetFloat("BgmSound", Mathf.Log10(val) * 20);
    }
    public void SwordSoundVolume(float val)
    {
        audiomixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20);
    }

}
