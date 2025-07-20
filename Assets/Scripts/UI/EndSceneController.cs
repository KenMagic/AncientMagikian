using TMPro; 
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text timeText;
    public TMP_Text waveText;

    void Start()
    {
        resultText.text = "YOU WIN!";
        timeText.text = "Time Lived: 1221s";
        waveText.text = "Highest Wave: 2";
    }

    public void OnTryAgainClick()
    {
        SceneManager.LoadScene("MapScenceOld");
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
