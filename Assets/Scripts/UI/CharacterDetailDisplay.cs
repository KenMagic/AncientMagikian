using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailDisplay : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI statsText;
    public Button selectButton;

    private PlayerStatsSO selectedStats;

    public void Display(PlayerStatsSO stats)
    {
        selectedStats = stats;
        characterImage.sprite = stats.portrait;
        characterName.text = stats.characterName;
        statsText.text = $"HP: {stats.maxHealth}\nAttack: {stats.damage}\nMovespeed: {stats.moveSpeed}";
    }

    public void SetupSelectButton(System.Action<PlayerStatsSO> onSelect)
    {
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => onSelect?.Invoke(selectedStats));
    }
}
