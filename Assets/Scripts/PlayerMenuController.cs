using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{

    public int selectedDrones;
    public bool[] drones;
    public Image[] dronesImage;
    public Text[] description;
    public Button prevButton, nextButton;
    public GameObject[] dronesSelector;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerVariables();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void StartGame(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void InitializePlayerVariables(){
        drones = GameController.instance.drones;
        selectedDrones = GameController.instance.selectedDrones;
        score.text = "" + GameController.instance.highScore.ToString("N0");
        for(int i = 1; i < drones.Length; i++){
            if(drones[i] == true){
                description[i - 1].gameObject.SetActive(false);
                dronesImage[i - 1].gameObject.GetComponent<Image>().color = Color.white;
            }
        }

        if(selectedDrones == 0){
            prevButton.gameObject.SetActive(false);
        }else if(selectedDrones == drones.Length - 1){
            nextButton.gameObject.SetActive(false);
        }

        for(int i = 0; i < drones.Length; i++){
            if(i == selectedDrones){
                dronesSelector[i].gameObject.SetActive(true);
            }else{
                dronesSelector[i].gameObject.SetActive(false);
            }
        }
    }

    public void NextButton(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        selectedDrones++;
        if(selectedDrones == drones.Length - 1){
            nextButton.gameObject.SetActive(false);
        }else{
            prevButton.gameObject.SetActive(true);
        }
        for(int i = 0; i < drones.Length; i++){
            if(i == selectedDrones){
                dronesSelector[i].gameObject.SetActive(true);
            }else{
                dronesSelector[i].gameObject.SetActive(false);
            }
        }
        if(drones[selectedDrones] == true){
            GameController.instance.selectedDrones = selectedDrones;
            GameController.instance.Save();
        }
    }

    public void PrevButton(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.ClickSound();
        }
        selectedDrones--;
        if(selectedDrones == 0){
            prevButton.gameObject.SetActive(false);
        }else{
            nextButton.gameObject.SetActive(true);
        }
        for(int i = 0; i < drones.Length; i++){
            if(i == selectedDrones){
                dronesSelector[i].gameObject.SetActive(true);
            }else{
                dronesSelector[i].gameObject.SetActive(false);
            }
        }
        if(drones[selectedDrones] == true){
            GameController.instance.selectedDrones = selectedDrones;
            GameController.instance.Save();
        }
    }
}
