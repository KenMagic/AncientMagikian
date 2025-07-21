using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneControl : MonoBehaviour
{
    public GameObject spawnEffect;
    public GameObject xpBar;

    GameObject playerPrefab;

    public Transform spawnPoint;

    public UpgradeUIManager upgradeUI;
    public int currentExp = 0;
    public int currentLevel = 1;
    public int expToNextLevel = 100;

    public static GameSceneControl Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        playerPrefab = GameController.Instance.CurrentPlayer.playerPrefab;
        StartCoroutine(PlaySummon());
    }

    IEnumerator PlaySummon()
    {
        GameObject eff = Instantiate(spawnEffect, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        Destroy(eff);
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        CameraFollow main = Camera.main.GetComponent<CameraFollow>();
        main.SetTarget(player.transform);
        xpBar.GetComponent<GameUI>().UpdateXPBar(currentExp, expToNextLevel);
        WaveManager.Instance.StartWaves();
    }

    public void ShowUpgrade()
    {
        GameController.Instance.PauseGame(); 
        StartCoroutine(ShowUpgradeAfter(2f));
    }
    public void AddExp(int amount)
    {
        currentExp += amount;
        CheckExp();
    }
    void LevelUp()
    {
        currentLevel++;
        expToNextLevel += 50; // Tăng dần yêu cầu EXP
        GameController.Instance.HighestLevel = currentLevel;
        ShowUpgrade();
    }

    private IEnumerator ShowUpgradeAfter(float v)
    {
        yield return new WaitForSecondsRealtime(v);
        upgradeUI.gameObject.SetActive(true);
        upgradeUI.ShowRandomUpgrades(3);
    }

    public void CheckExp()
    {
        if (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();
            Debug.Log("Level up" + currentLevel);
        }
        xpBar.GetComponent<GameUI>().UpdateXPBar(currentExp, expToNextLevel);
    }

}
