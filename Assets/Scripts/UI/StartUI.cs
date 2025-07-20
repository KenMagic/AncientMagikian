using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button SoundBtn;
    [SerializeField]
    private Sprite SoundOn;
    [SerializeField]
    private Sprite SoundOff;
    private bool isSoundOn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartBtn.onClick.AddListener(StartGame);
        SoundBtn.onClick.AddListener(ToggleSound);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        SceneManager.LoadScene("ChooseHero");
    }
    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        UpdateSoundIcon();
    }

    void UpdateSoundIcon()
    {
        var image = SoundBtn.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = isSoundOn ? SoundOn : SoundOff;
        }
    }
}
