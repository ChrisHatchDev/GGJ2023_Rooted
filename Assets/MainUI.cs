using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;

    public Base BaseInScene;
    public Image BaseHealthbarFill;

    public Transform _gameOverScreen;
    public Transform _inGameUIScreen;

    public static Transform GameOverScreen;
    public static Transform InGameUIScreen;

    private void Start()
    {
        Instance = this;

        GameOverScreen = _gameOverScreen;
        InGameUIScreen = _inGameUIScreen;
    }

    void Update()
    {
        BaseHealthbarFill.fillAmount = BaseInScene.Health / BaseInScene.MaxHealth;
    }

    public static void ShowGameOverScreen()
    {
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
