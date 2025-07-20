using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneController : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text timeText;
    public TMP_Text waveText;

    void Start()
    {
        resultText.text = GameController.Instance.GameWin ? "You Win" : "You Lose";
        timeText.text = "Highest level: " + GameController.Instance.HighestLevel;
        waveText.text = "Highest Wave: " + GameController.Instance.HighestWave;
    }

    public void OnTryAgainClick()
    {
        GameController.Instance.Restart();
    }

    public void OnMainMenuClick()
    {
        GameController.Instance.ChangeToMainMenu();
    }
}
