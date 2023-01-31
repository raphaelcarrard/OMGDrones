using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public GameObject exitPanel, settingPanel, howToPlayPanel;
    public Toggle musicToggle;
    public Text control;
    public GameObject[] infos;
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(GameController.instance.isMusicOn){
            MusicController.instance.PlayerBGMusic();
            musicToggle.isOn = true;
        }else{
            MusicController.instance.StopBGMusic();
            musicToggle.isOn = false;
        }
        if(GameController.instance.controls){
            control.text = "TILT";
        }else{
            control.text = "TOUCH";
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(exitPanel.activeInHierarchy){
                exitPanel.SetActive(false);
            }else{
                exitPanel.SetActive(true);
            }

            if(settingPanel.activeInHierarchy){
                settingPanel.SetActive(false);
            }
        }
    }

    public void PlayGame(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        SceneManager.LoadScene("PlayerMenu");
    }

    public void YesButton(){
        Application.Quit();
        //SceneManager.LoadScene("ThanksForPlaying");(this code line is only for WebGL)
    }

    public void NoButton(){
        exitPanel.SetActive(false);
    }

    public void SettingsButton(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        settingPanel.SetActive(true);
    }

        public void HowToPlayButton(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        howToPlayPanel.SetActive(true);
    }

    public void CloseButton(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        settingPanel.SetActive(false);
    }

    public void CloseInfo(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        howToPlayPanel.SetActive(false);
    }

    public void MusicToggle(){
        if(musicToggle.isOn){
            GameController.instance.isMusicOn = true;
            MusicController.instance.PlayerBGMusic();
            GameController.instance.Save();
        }else{
            GameController.instance.isMusicOn = false;
            MusicController.instance.StopBGMusic();
            GameController.instance.Save();
        }
    }

    public void Controls(){
        if(GameController.instance.controls){
            control.text = "TOUCH";
            GameController.instance.controls = false;
            GameController.instance.Save();
        }else{
            control.text = "TILT";
            GameController.instance.controls = true;
            GameController.instance.Save();
        }
    }

    public void NextButton(){
        count++;
        if(count == infos.Length){
            count = 0;
        }
        for(int i = 0; i < infos.Length; i++){
            if(count == i){
                infos[i].SetActive(true);
            }else{
                infos[i].SetActive(false);
            }
        }
    }

    public void PrevButton(){
        count--;
        if(count <= 0 - 1){
            count = infos.Length - 1;
        }
        for(int i = 0; i < infos.Length; i++){
            if(count == i){
                infos[i].SetActive(true);
            }else{
                infos[i].SetActive(false);
            }
        }
    }
}
