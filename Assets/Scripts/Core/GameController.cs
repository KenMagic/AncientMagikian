using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public PlayerStatsSO CurrentPlayer;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    private GameState currentState = GameState.MainMenu;
    public GameState CurrentState => currentState;

    private int coin;

    public bool GameWin { get; set; } = false;

    public int HighestWave { get; set; } = 0;
    public int HighestLevel { get; set; } = 0;

    public List<UpgradeSO> allUpgrades;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeToMainMenu();
    }

    // ========================
    // Game State Control API
    // ========================

    public void ChangeToMainMenu()
    {     
        ResetStatus(false);
        SceneManager.LoadScene("StartScene");
        AudioManager.Instance.PlayBGM(AudioManager.Instance.mainTheme);
        ChangeState(GameState.MainMenu);
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.PlayBGM(AudioManager.Instance.battleTheme);
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
            ChangeState(GameState.Playing);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("EndScene");
        if (GameWin)
        {
            AudioManager.Instance.PlayBGM(AudioManager.Instance.victoryTheme);
        }
        else
        {
            AudioManager.Instance.PlayBGM(AudioManager.Instance.loseTheme);
        }
        ChangeState(GameState.GameOver);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
        ChangeState(GameState.Playing);
        ResetStatus(true);
        Time.timeScale = 1f;
    }

    private void ChangeState(GameState newState)
    {
        if (newState == currentState) return;
        currentState = newState;
        Debug.Log("Changed state to: " + currentState);
    }

    private void ResetStatus(bool isRestart)
    {
        if (!isRestart)
        {
            CurrentPlayer = null;
            allUpgrades = null;
        }
        GameWin = false;
        HighestWave = 0;
        HighestLevel = 0;
    }

    // ========================
    // Coin API
    // ========================

    public void AddCoin(int amount)
    {
        coin += amount;
        Debug.Log("Coins: " + coin);
    }

    public int GetCoin()
    {
        return coin;
    }

    public void ResetCoin()
    {
        coin = 0;
    }
}
