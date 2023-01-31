using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    public bool gameInProgress, isGamePaused;
    public GameObject pausePanel, gameoverPanel;
    public Text scoreText, gameoverScore, highScoreText, unlockText;
    public Slider batteryLimit;
    public float batteryBurn;
    public int scoreMultiplier;
    public GameObject[] drone;
    public GameObject background, obstacleSpawner;

    private Vector3 startPos;
    private float score = 0f;
    private float battery = 100f;
    private GameObject player;

    public io.newgrounds.core ngio_core;

    public void NGSubmitScore(int score_id, int score){
        io.newgrounds.components.ScoreBoard.postScore submit_score = new io.newgrounds.components.ScoreBoard.postScore();
        submit_score.id = score_id;
        submit_score.value = score;
        submit_score.callWith(ngio_core);
        Debug.Log("Scoreboard Added");
    }

    void Awake(){
        CreateInstance();
        gameInProgress = true;
        InitializeBatteryVariable();
    }

    void CreateInstance(){
        if(instance == null){
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameInProgress){
            UpdateGameplay();
            BatteryStatus();
        }
    }

    void InitializePlayer(){
        if(GameController.instance.isMusicOn){
            MusicController.instance.GameplayBGMusic();
        }
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.transform.position.z + 10));
        startPos.y = pos.y;
        GameObject player = Instantiate(drone[GameController.instance.selectedDrones], new Vector3(startPos.x, startPos.y - 8, startPos.z), Quaternion.identity) as GameObject;
        if(GameController.instance.isMusicOn){
            player.GetComponent<AudioSource>().Play();
        }
        this.player = player;
        background.GetComponent<Background>().speed = 0.5f;
        obstacleSpawner.GetComponent<ObstacleSpawner>().delay = 5;
    }

    void Difficulty(){
        if(GameController.instance.currentScore <= 5000 && GameController.instance.currentScore >= 5000){
            background.GetComponent<Background>().speed = 0.6f;
            obstacleSpawner.GetComponent<ObstacleSpawner>().delay = 4f;
        }
        if(GameController.instance.currentScore <= 15000 && GameController.instance.currentScore >= 15000){
            background.GetComponent<Background>().speed = 0.7f;
            obstacleSpawner.GetComponent<ObstacleSpawner>().delay = 3f;
        }
        if(GameController.instance.currentScore <= 45000 && GameController.instance.currentScore >= 45000){
            background.GetComponent<Background>().speed = 0.8f;
            obstacleSpawner.GetComponent<ObstacleSpawner>().delay = 2f;
        }
        if(GameController.instance.currentScore <= 70000 && GameController.instance.currentScore >= 70000){
            background.GetComponent<Background>().speed = 0.9f;
            obstacleSpawner.GetComponent<ObstacleSpawner>().delay = 1f;
        }
        if(GameController.instance.currentScore <= 100000 && GameController.instance.currentScore >= 100000){
            background.GetComponent<Background>().speed = 1f;
            obstacleSpawner.GetComponent<ObstacleSpawner>().delay = 0.85f;
        }
    }

    public void PauseGame(){
        if(gameInProgress){
            isGamePaused = true;
            gameInProgress = false;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            if(GameController.instance.isMusicOn){
                player.GetComponent<AudioSource>().Pause();
            }
        }
    }

    public void ContinueGame(){
        if(isGamePaused){
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            gameInProgress = true;
            isGamePaused = false;
            player.GetComponent<AudioSource>().UnPause();
        }
    }

    public void RestartGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame(){
        Time.timeScale = 1;
        if(GameController.instance.isMusicOn){
            MusicController.instance.StopBGMusic();
            MusicController.instance.PlayerBGMusic();
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void GiveUpGame(){
        Time.timeScale = 1;
        PlayerDied(player);
    }

    void UpdateGameplay(){
        score += scoreMultiplier * Time.deltaTime;
        GameController.instance.currentScore = (int)score;
        scoreText.text = "" + (int)score;
        RewardNewDrone();
        Difficulty();
    }

    void RewardNewDrone(){
        if(!GameController.instance.drones[1]){
            if(GameController.instance.currentScore >= 50000){
                StartCoroutine(UnlockDrones());
                GameController.instance.drones[1] = true;
                GameController.instance.Save();
            }
        }
        if(!GameController.instance.drones[2]){
            if(GameController.instance.currentScore >= 100000){
                StartCoroutine(UnlockDrones());
                GameController.instance.drones[2] = true;
                GameController.instance.Save();
            }
        }
    }

    IEnumerator UnlockDrones(){
        unlockText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        unlockText.gameObject.SetActive(false);
    }

    void InitializeBatteryVariable(){
        batteryLimit.maxValue = battery;
        batteryLimit.value = batteryLimit.maxValue;
    }

    void BatteryStatus(){
        if(battery > 0){
            battery -= batteryBurn * Time.deltaTime;
            batteryLimit.value = battery;
        }else{
            PlayerDied(player);
        }
    }

    public void BatteryCharge(int charge){
        battery += charge;
        if(battery >= 100f){
            battery = 100f;
        }
    }

    public void PlayerDied(GameObject player){
        if(GameController.instance.isMusicOn){
            AudioSource.PlayClipAtPoint(MusicController.instance.audioClips[3], player.transform.position);
        }
        gameInProgress = false;
        Instantiate(player.GetComponent<PlayerController>().explosion.gameObject, player.transform.position, Quaternion.identity);
        Destroy(player);
        GameOver();
    }

    void GameOver(){
        gameoverPanel.SetActive(true);
        gameoverScore.text = "" + GameController.instance.currentScore.ToString("N0");
        NGSubmitScore(12515, GameController.instance.currentScore);
        if(GameController.instance.currentScore >= GameController.instance.highScore){
            highScoreText.gameObject.SetActive(true);
            GameController.instance.highScore = GameController.instance.currentScore;
            GameController.instance.Save();
        }
    }
}
