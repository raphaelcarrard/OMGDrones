using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public static MusicController instance;
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Awake(){
        MakeInstance();
        audioSource = GetComponent<AudioSource>();
    }

    void MakeInstance(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerBGMusic(){
        AudioClip bgMusic = audioClips[0];
        if(bgMusic){
            if(!audioSource.isPlaying){
                audioSource.clip = bgMusic;
                audioSource.loop = true;
                audioSource.volume = 1f;
                audioSource.Play();
            }
        }
    }

    public void GameplayBGMusic(){
        AudioClip gameplayMusic = audioClips[1];
        if(gameplayMusic){
                audioSource.clip = gameplayMusic;
                audioSource.loop = true;
                audioSource.volume = 1f;
                audioSource.Play();
        }
    }

    public void StopBGMusic(){
        if(audioSource.isPlaying){
            audioSource.Stop();
        }
    }

    public void ClickSound(){
        audioSource.PlayOneShot(audioClips [2], 1f);
    }

}
