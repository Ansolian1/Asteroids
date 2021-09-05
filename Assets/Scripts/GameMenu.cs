using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private KeyboardInput keyboard;
    [SerializeField]
    private KeyboardAndMouseInput keyboardAndMouse;
    [SerializeField]
    private GameObject gameMenu;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private Text controls;

    [SerializeField]
    private Text score;
    [SerializeField]
    private Text lives;

    [SerializeField]
    private PlayerSpawner playerSpawner;
    [SerializeField]
    private ScoreManager scoreManager;

    void Awake()
    {
        PauseGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            continueButton.SetActive(true);
            PauseGame();
        }
    }

    public void SetLivesCount(int livesCount)
    {
        lives.text = livesCount.ToString();
    }

    public void SetScoreCount(int scoreCount)
    {
        score.text = scoreCount.ToString();
    }

    public void GameOver()
    {
        continueButton.SetActive(false);
        PauseGame();
    }

    private void PauseGame()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueButtonClick()
    {
        gameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void NewGameButtonClick()
    {
        playerSpawner.RestartGame();
        GlobalAccess.Instance.playerShip.NullifyVelocity();
        GlobalAccess.Instance.bulletsPool.AllToPool();
        GlobalAccess.Instance.enemyBulletsPool.AllToPool();
        GlobalAccess.Instance.asteroidsSpawner.RestartGame();
        GlobalAccess.Instance.ufoSpawner.RestartGame();
        scoreManager.BreakScore();
        gameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ControlsButtonClick()
    {
        if (keyboard.enabled)
        {
            keyboard.enabled = false;
            keyboardAndMouse.enabled = true;
            controls.text = "”правление: клавиатура + мышь";
        }
        else
        {
            keyboard.enabled = true;
            keyboardAndMouse.enabled = false;
            controls.text = "”правление: клавиатура";
        }
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

}
