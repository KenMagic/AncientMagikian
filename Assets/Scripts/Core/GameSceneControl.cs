using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneControl : MonoBehaviour
{
    public GameObject spawnEffect;

    GameObject playerPrefab;

    public Transform spawnPoint;

    public UpgradeUIManager upgradeUI;
    public Button upgradeBtnTest;
    public int currentExp = 0;
    public int currentLevel = 1;
    public int expToNextLevel = 100;



    void Start()
    {
        playerPrefab = GameController.Instance.CurrentPlayer.playerPrefab;
        StartCoroutine(PlaySummon());
        upgradeBtnTest.onClick.AddListener(ShowUpgrade);
    }

    IEnumerator PlaySummon()
    {
        GameObject eff = Instantiate(spawnEffect, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);
        Destroy(eff);
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void ShowUpgrade()
    {
        GameController.Instance.PauseGame();
        upgradeUI.gameObject.SetActive(true);
        upgradeUI.ShowRandomUpgrades(3);
    }
    public void AddExp(int amount)
{
    currentExp += amount;

    while (currentExp >= expToNextLevel)
    {
        currentExp -= expToNextLevel;
        LevelUp();
    }
}
void LevelUp()
{
    currentLevel++;
    expToNextLevel += 50; // Tăng dần yêu cầu EXP

    Debug.Log($"Level Up! Current Level: {currentLevel}");

        ShowUpgrade();
}

}
