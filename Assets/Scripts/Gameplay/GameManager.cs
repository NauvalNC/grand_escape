/*
 * Author Information
 * Nama	: Nauval Muhammad Firdaus
 * NIM	: 2301906331
 * Kelas	: LB04 (Kelas Kecil) / MA04 (Kelas Besar)
 * Matkul	: Game Programming (GAME6069)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get { if (instance == null) instance = FindObjectOfType<GameManager>(); return instance; }
    }

    [Header("Game Management")]
    public float gameSpeed = 1f;
    public float gameSpeedPlusFactor = 1f;
    public int distanceSpeedFactor = 10;
    public Animator countDownAC;
    public Text countText;
    public int countDown = 3;
    bool gameStart = false;
    float init_spawnerSpeed = 0, init_bgSpeed = 0, init_gravityScale, init_playerRunSpeed;
    int lastDistance = 0;

    [Header("UI Management")]
    public float panelExpansion = 15f;
    public Animator pauseAC;

    [Header("Score Header")]
    public Text scoreTxt;
    public RectTransform scorePanel;
    int score = 0;
    Vector2 scorePanelDelta;

    [Header("Distance Traveled")]
    public Text distText;
    public RectTransform distPanel;
    public float distancePerMeter = 10f;
    public int modAtDigit = 8;
    float distance = 0;
    Vector2 distPanelDelta;

    [Header("Sound Manager")]
    public AudioSource pointSound;
    public AudioSource buttonSound;

    #region Speed to Control Variables

    Player player;
    Animator playerAC;
    ParallaxBG background;
    LevelSpawner spawner;
    
    #endregion

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerAC = player.GetComponent<Animator>();
        background = FindObjectOfType<ParallaxBG>();
        spawner = FindObjectOfType<LevelSpawner>();
        scorePanelDelta = scorePanel.sizeDelta;
        distPanelDelta = distPanel.sizeDelta;

        player.IsEnabled = false;
        spawner.IsEnabled = false;
        background.IsEnabled = false;

        init_gravityScale = player.gravityScale;
        init_spawnerSpeed = spawner.speed;
        init_bgSpeed = background.speed;
        init_playerRunSpeed = playerAC.speed;

        ApplyGameSpeedToGameplay();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (gameStart) 
        {
            CalcDistanceTraveled();
            GenerateGameSpeed();
        }

        KeyDownListener();

        EventSystem.current.SetSelectedGameObject(null);
    }

    void KeyDownListener() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }

    void GenerateGameSpeed() 
    {
        if (Time.timeScale == 0) return;

        if ((int)distance > lastDistance && (int)distance % distanceSpeedFactor == 0) 
        {
            lastDistance = (int)distance;
            gameSpeed += gameSpeedPlusFactor;
            ApplyGameSpeedToGameplay();
        }
    }

    void ApplyGameSpeedToGameplay() 
    {
        spawner.speed = init_spawnerSpeed * gameSpeed;
        background.speed = init_bgSpeed * gameSpeed;
        player.gravityScale = init_gravityScale * gameSpeed;
        playerAC.speed = init_playerRunSpeed * gameSpeed;
    }

    public void StartGame() 
    {
        StartCoroutine(ICountDown());
    }

    IEnumerator ICountDown() 
    {
        for (int i = 0; i < countDown; i++) 
        {
            countDownAC.gameObject.SetActive(true);
            countText.text = (countDown - i).ToString();
            yield return new WaitForSecondsRealtime(1f);
            countDownAC.gameObject.SetActive(false);
        }

        gameStart = true;
        player.IsEnabled = true;
        spawner.IsEnabled = true;
        background.IsEnabled = true;

        Time.timeScale = 1;
    }

    void CalcDistanceTraveled() 
    {
        distance += ((1 * gameSpeed) / distancePerMeter) * Time.deltaTime;
        distText.text = ((int)distance).ToString() + "m";

        int digit = CalcDigit((int)distance);
        if (digit > 7) 
        {
            distPanel.sizeDelta = new Vector2(distPanelDelta.x + panelExpansion * (digit % modAtDigit), distPanelDelta.y);
        }
    }

    public void AddScore() 
    {
        pointSound.Play();
        score += 1;
        scoreTxt.text = score.ToString();
        scorePanel.sizeDelta = new Vector2(scorePanelDelta.x + panelExpansion * CalcDigit(score), scorePanelDelta.y);
    }

    int CalcDigit(int num)
    {
        int count = 0;
        while(num != 0) 
        {
            num /= 10;
            count++;
        }
        return count;
    }

    bool togglePause = false;
    public void Pause() 
    {
        buttonSound.Play();
        StartCoroutine(IPause());    
    }

    IEnumerator IPause() 
    {
        if (pauseAC.gameObject.activeInHierarchy == false) pauseAC.gameObject.SetActive(true);

        togglePause = !togglePause;
        pauseAC.SetBool("toggle", togglePause);

        if (togglePause == false) 
        {
            float wait = pauseAC.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSecondsRealtime(wait);
            StartCoroutine(ICountDown());
        } else 
        {
            Time.timeScale = 0;
        }
    }

    private void OnDestroy()
    {
        ScoreManager.SaveScore(score, (int)distance);
        Time.timeScale = 1;
    }

    public void Restart() 
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void InvokeGameOver() 
    {
        SceneManager.LoadScene("GameOver");
    }

    public bool IsGameStart { get { return gameStart; } }
}
