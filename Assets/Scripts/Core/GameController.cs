using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public PlayerStatsSO currentPlayer { get; set; }

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
        ChangeState(GameState.MainMenu);
    }

    public void StartGame()
    {
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
            ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    private void ChangeState(GameState newState)
    {
        if (newState == currentState) return;

        currentState = newState;
        Debug.Log("Changed state to: " + currentState);
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
