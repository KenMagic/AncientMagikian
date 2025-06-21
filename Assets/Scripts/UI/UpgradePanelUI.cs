using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tittleText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;

    [SerializeField] public UpgradeSO upgradeData;

    public Image background;

    private void Start()
    {
        UpdateUpgradeUI();
    }

    private void UpdateUpgradeUI()
    {
        tittleText.text = upgradeData.upgradeName;
        upgradeDescriptionText.text = upgradeData.description;
        background.color = GetColorByType(upgradeData.upgradeType);
    }

    private void OnUpgradeButtonClicked()
    {
        // Logic to handle the upgrade, e.g., checking if player can afford it
        Debug.Log("Upgrade button clicked!");
        // Implement actual upgrade logic here
    }
    private Color GetColorByType(UpgradeSO.UpgradeType type)
    {
        return type switch
        {
            UpgradeSO.UpgradeType.Common => new Color32(200, 200, 200, 255), // Gray
            UpgradeSO.UpgradeType.Rare => new Color32(80, 150, 255, 255),    // Blue
            UpgradeSO.UpgradeType.Epic => new Color32(180, 80, 255, 255),    // Purple
            UpgradeSO.UpgradeType.Legendary => new Color32(255, 180, 0, 255),// Orange
            _ => Color.white,
        };
    }

    public void SetUpgradeData(UpgradeSO upgrade)
    {
        upgradeData = upgrade;
        UpdateUpgradeUI();
    }
}