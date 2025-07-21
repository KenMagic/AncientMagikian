using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    private Button StartBtn;
    [SerializeField]
    private Button SettingBtn;
    public GameObject SettingUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartBtn.onClick.AddListener(StartGame);
        SettingBtn.onClick.AddListener(ToggleSetting);
    }

    private void ToggleSetting()
    {
        SettingUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        SceneManager.LoadScene("ChooseHero");
    }
}
