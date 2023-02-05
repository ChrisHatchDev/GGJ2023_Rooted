using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    public static bool GameIsActive = false;

    public Base BaseInScene;
    public Image BaseHealthbarFill;

    public Transform _gameOverScreen;
    public Transform _inGameUIScreen;
    public Transform _startScreen;

    public static Transform GameOverScreen;
    public static Transform InGameUIScreen;
    public static Transform StartScreen;

    private void Start()
    {
        Instance = this;

        GameOverScreen = _gameOverScreen;
        InGameUIScreen = _inGameUIScreen;
        StartScreen = _startScreen;

        Application.targetFrameRate = 90;
    }

    void Update()
    {
        BaseHealthbarFill.fillAmount = BaseInScene.Health / BaseInScene.MaxHealth;
    }

    public static void ShowGameOverScreen()
    {
        GameIsActive = false;
        GameOverScreen.gameObject.SetActive(true);
        InGameUIScreen.gameObject.SetActive(false);
    }

    public static void ShowInGameUI()
    {
        GameOverScreen.gameObject.SetActive(false);
        InGameUIScreen.gameObject.SetActive(true);
    }

    public void TryAgain()
    {
        EnemySpawner.CurrentWave = 0;
        EnemySpawner.NumberOfEnemiesKilled = 0;

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        StartScreen.gameObject.SetActive(false);
        ShowInGameUI();
        GameIsActive = true;
    }
}
