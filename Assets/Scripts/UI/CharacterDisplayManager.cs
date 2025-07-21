using UnityEngine;
using System.Collections.Generic;

public class CharacterSelectManager : MonoBehaviour
{
    public List<PlayerStatsSO> availableCharacters;
    public List<PlayerUpgradeMap> playerUpgradeMappings;

    private Dictionary<PlayerStatsSO, List<UpgradeSO>> upgradeDict;
    public GameObject thumbnailPrefab;
    public Transform thumbnailParent;
    public CharacterDetailDisplay detailDisplay;
    void Awake()
    {
        upgradeDict = new Dictionary<PlayerStatsSO, List<UpgradeSO>>();
        foreach (var map in playerUpgradeMappings)
        {
            if (!upgradeDict.ContainsKey(map.playerSO))
            {
                upgradeDict.Add(map.playerSO, map.upgrades);
            }
        }
    }
    public List<UpgradeSO> GetUpgradesFor(PlayerStatsSO player)
    {
        return upgradeDict.TryGetValue(player, out var list) ? list : null;
    }

    void Start()
    {
        foreach (var stats in availableCharacters)
        {
            GameObject item = Instantiate(thumbnailPrefab, thumbnailParent);
            item.transform.SetParent(thumbnailParent);
            item.GetComponent<CharacterImageButton>().Setup(stats, OnCharacterSelected);
        }

        detailDisplay.SetupSelectButton(OnCharacterChosen);
    }

    void OnCharacterSelected(PlayerStatsSO stats)
    {
        detailDisplay.Display(stats);
    }

    void OnCharacterChosen(PlayerStatsSO stats)
    {
        Debug.Log("Player selected: " + stats.characterName);
        GameController.Instance.CurrentPlayer = stats;
        GameController.Instance.StartGame();
        GameController.Instance.allUpgrades = GetUpgradesFor(stats);
    }
}
