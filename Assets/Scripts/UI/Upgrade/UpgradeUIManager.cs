using System.Collections.Generic;
using UnityEngine;

public class UpgradeUIManager : MonoBehaviour
{
    [Header("Upgrade UI")]
    [SerializeField] private UpgradeCardUI upgradeCardUIPrefab;
    [SerializeField] private Transform cardContainer;

    [Header("Upgrade Sources")]
    [SerializeField] private List<UpgradeSO> allUpgrades;

    [Header("Rarity Chances (total = 100%)")]
    [Range(0, 100)] public int commonChance = 50;
    [Range(0, 100)] public int rareChance = 30;
    [Range(0, 100)] public int epicChance = 15;
    [Range(0, 100)] public int legendaryChance = 5;

    private GameObject player;

    public static UpgradeUIManager Instance { get; private set; }

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

    private void Start()
    {

    }

    public void ShowRandomUpgrades(int count)
    {
        SetUpgrades();
        player = GameObject.FindGameObjectWithTag("Player");
        // Clear old UI
        foreach (Transform child in cardContainer)
            Destroy(child.gameObject);

        List<UpgradeSO> availableUpgrades = new List<UpgradeSO>(allUpgrades);
        List<UpgradeSO> selectedUpgrades = new List<UpgradeSO>();

        for (int i = 0; i < count; i++)
        {
            UpgradeSO upgrade = GetRandomUpgradeNoDuplicate(availableUpgrades);
            if (upgrade == null) continue;

            selectedUpgrades.Add(upgrade);
            availableUpgrades.Remove(upgrade);

            UpgradeCardUI ui = Instantiate(upgradeCardUIPrefab, cardContainer);
            ui.Setup(upgrade, player);
        }
    }

    private UpgradeSO GetRandomUpgradeNoDuplicate(List<UpgradeSO> pool)
    {
        if (pool == null || pool.Count == 0) return null;

        UpgradeSO.UpgradeType rarity = GetRandomRarity();

        // Filter by rarity
        List<UpgradeSO> filtered = pool.FindAll(u => u.upgradeType == rarity);

        // If none found for that rarity, fallback to entire pool
        if (filtered.Count == 0)
            filtered = pool;

        if (filtered.Count == 0)
            return null;

        return filtered[Random.Range(0, filtered.Count)];
    }

    private UpgradeSO.UpgradeType GetRandomRarity()
    {
        int roll = Random.Range(0, 100);
        if (roll < legendaryChance)
            return UpgradeSO.UpgradeType.Legendary;
        else if (roll < legendaryChance + epicChance)
            return UpgradeSO.UpgradeType.Epic;
        else if (roll < legendaryChance + epicChance + rareChance)
            return UpgradeSO.UpgradeType.Rare;
        else
            return UpgradeSO.UpgradeType.Common;
    }
    public void HideUpgradeUI()
    {
        gameObject.SetActive(false);
        GameController.Instance.ResumeGame();
        GameSceneControl.Instance.CheckExp();
    }

    public void SetUpgrades()
    {
        allUpgrades = GameController.Instance.allUpgrades;
    }
}
