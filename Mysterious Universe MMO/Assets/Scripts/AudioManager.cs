using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource levelMusic, bossMusic, victoryMusic, loseMusic;//các music
    public AudioSource[] audioSFX;//list sfx

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelMusic.Play();
    }

    // Update is called once per frame

    private void StopAll()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        loseMusic.Stop();
    }

    public void LevelMusic()
    {
        StopAll();
        levelMusic.Play();
    }

    public void BossMusic()
    {
        StopAll();
        bossMusic.Play();
    }

    public void VictoryMusic()
    {
        StopAll();
        victoryMusic.Play();
    }

    public void LoseMusic()
    {
        StopAll();
        loseMusic.Play();
    }

    public void PlaySFX(int sfx)
    {
        audioSFX[sfx].Stop();
        audioSFX[sfx].Play();
    }
}
