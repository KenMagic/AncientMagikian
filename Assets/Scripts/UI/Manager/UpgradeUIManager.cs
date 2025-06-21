using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UpgradeUIManager : MonoBehaviour
{
    //list of upgrades
    public UpgradeSO[] upgrades;
    //UI elements
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;
    public GameObject upgradePanel3;

    //refresh the UI button
    public Button refreshButton;

    void Start()
    {
        // Load upgrades from resources
        LoadUpgrades();
        GetRandomUpgrades();
    }

    void LoadUpgrades()
    {
        upgrades = Resources.LoadAll<UpgradeSO>("Upgrades");
    }

    // get 3 random upgrades from the list
    public void GetRandomUpgrades()
    {
        if (upgrades.Length < 3)
        {
            Debug.LogWarning("Not enough upgrades available to display.");
            return;
        }

        UpgradeSO[] selectedUpgrades = upgrades
                                            .OrderBy(x => Random.value)
                                            .Take(3)
                                            .ToArray();

        upgradePanel1.GetComponent<UpgradePanelUI>().SetUpgradeData(selectedUpgrades[0]);
        upgradePanel2.GetComponent<UpgradePanelUI>().SetUpgradeData(selectedUpgrades[1]);
        upgradePanel3.GetComponent<UpgradePanelUI>().SetUpgradeData(selectedUpgrades[2]);
    }

    public void Refresh()
    {
        GetRandomUpgrades();
    }

}