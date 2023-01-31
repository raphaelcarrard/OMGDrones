using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public bool isMusicOn;
    public bool isGameStartedFirstTime;
    public bool controls;

    public int currentScore;
    public int selectedDrones;
    public int highScore;

    public bool[] drones;

    private GameData data;

    void Awake(){
        MakeInstance();
        InitializeGameVariables();
    }

    void MakeInstance(){
        if(instance != null){
            Destroy(gameObject);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameVariables(){
        Load();
        if(data != null){
            isGameStartedFirstTime = data.GetIsGameStartedFirstTime();
        }else{
            isGameStartedFirstTime = true;
        }

        if(isGameStartedFirstTime){
            highScore = 0;
            isMusicOn = true;
            selectedDrones = 0;
            isGameStartedFirstTime = false;
            controls = true;
            drones = new bool[3];
            drones [0] = true;
            for(int i = 1; i < drones.Length; i++){
                drones[i] = false;
            }
            data = new GameData();
            data.SetHighScore(highScore);
            data.SetDrones(drones);
            data.SetIsGameStartedFirstTime(isGameStartedFirstTime);
            data.SetMusicIsOn(isMusicOn);
            data.SetSelectedDrones(selectedDrones);
            data.SetControls(controls);
            Save();
            Load();
        }else{
            highScore = data.GetHighScore();
            drones = data.GetDrones();
            selectedDrones = data.GetSelectedDrones();
            isMusicOn = data.GetMusicIsOn();
            isGameStartedFirstTime = data.GetIsGameStartedFirstTime();
            controls = data.GetControls();
        }
    }

    public void Save(){
        FileStream file = null;
        try{
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/data.dat");
            if(data != null){
                data.SetHighScore(highScore);
                data.SetDrones(drones);
                data.SetSelectedDrones(selectedDrones);
                data.SetControls(controls);
                data.SetMusicIsOn(isMusicOn);
                bf.Serialize(file, data);
            }
        }catch(Exception e){
            Debug.LogException(e, this);
        }finally{
            if(file != null){
                file.Close();
            }
        }
    }

    public void Load(){
        FileStream file = null;
        try{
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
        }catch(Exception e){
            Debug.LogException(e, this);
        }finally{
            if(file != null){
                file.Close();
            }
        }
    }
}

[Serializable]
class GameData{
    private bool isMusicOn;
    private bool isGameStartedFirstTime;
    private bool controls;

    private int highScore;
    private int selectedDrones;

    private bool[] drones;

    public void SetMusicIsOn(bool isMusicOn){
        this.isMusicOn = isMusicOn;
    }

    public bool GetMusicIsOn(){
        return this.isMusicOn;
    }

    public void SetIsGameStartedFirstTime(bool isGameStartedFirstTime){
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool GetIsGameStartedFirstTime(){
        return this.isGameStartedFirstTime;
    }

    public void SetHighScore(int highScore){
        this.highScore = highScore;
    }

    public int GetHighScore(){
        return this.highScore;
    }

    public void SetDrones(bool[] drones){
        this.drones = drones;
    }

    public bool[] GetDrones(){
        return this.drones;
    }

    public void SetSelectedDrones(int selectedDrones){
        this.selectedDrones = selectedDrones;
    }

    public int GetSelectedDrones(){
        return this.selectedDrones;
    }

    public void SetControls(bool controls){
        this.controls = controls;
    }

    public bool GetControls(){
        return this.controls;
    }
}
