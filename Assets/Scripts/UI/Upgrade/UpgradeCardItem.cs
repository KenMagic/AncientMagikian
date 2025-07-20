using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UpgradeCardUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private Button applyButton;

    private UpgradeSO upgradeItem;
    private GameObject target;

    public void Setup(UpgradeSO item, GameObject applyTarget)
    {
        upgradeItem = item;
        target = applyTarget;

        iconImage.sprite = item.icon;
        nameText.text = item.UpgradeName;
        descriptionText.text = item.Description;

        rarityText.text = item.upgradeType.ToString();
        rarityText.color = GetColorForRarity(item.upgradeType);

        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(OnApplyClicked);
    }

    private void OnApplyClicked()
    {
        upgradeItem.Apply(target);
        UpgradeUIManager.Instance.HideUpgradeUI();
    }

    private Color GetColorForRarity(UpgradeSO.UpgradeType type)
    {
        switch (type)
        {
            case UpgradeSO.UpgradeType.Common:
                return Color.white;
            case UpgradeSO.UpgradeType.Rare:
                return Color.blue;
            case UpgradeSO.UpgradeType.Epic:
                return new Color(0.6f, 0.2f, 0.8f); // purple
            case UpgradeSO.UpgradeType.Legendary:
                return new Color(1f, 0.6f, 0f); // orange-gold
            default:
                return Color.white;
        }
    }
}
