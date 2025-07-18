//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;

//public class UpgradeManager : MonoBehaviour
//{
//    public GameObject upgradeCanvas;
//    public List<PlayerSO> allUpgrades;
//    public Button[] upgradeButtons;

//    private PlayerStats playerStats;

//    void Start()
//    {
//        upgradeCanvas.SetActive(false);
//        playerStats = FindObjectOfType<PlayerStats>();
//    }

//    public void ShowUpgrades()
//    {
//        upgradeCanvas.SetActive(true);

//        List<PlayerSO> randomUpgrades = GetRandomUpgrades(3);
//        for (int i = 0; i < upgradeButtons.Length; i++)
//        {
//            int index = i;
//            var upgrade = randomUpgrades[i];

//            upgradeButtons[i].transform.GetChild(0).GetComponent<Text>().text = upgrade.upgradeName;
//            upgradeButtons[i].transform.GetChild(1).GetComponent<Image>().sprite = upgrade.icon;

//            upgradeButtons[i].onClick.RemoveAllListeners();
//            upgradeButtons[i].onClick.AddListener(() => ApplyUpgrade(upgrade));
//        }
//    }

//    List<PlayerSO> GetRandomUpgrades(int count)
//    {
//        List<PlayerSO> result = new List<PlayerSO>();
//        List<PlayerSO> copy = new List<PlayerSO>(allUpgrades);

//        for (int i = 0; i < count && copy.Count > 0; i++)
//        {
//            int index = Random.Range(0, copy.Count);
//            result.Add(copy[index]);
//            copy.RemoveAt(index);
//        }

//        return result;
//    }

//    void ApplyUpgrade(PlayerSO upgrade)
//    {
//        playerStats.ApplyUpgrade(upgrade);
//        upgradeCanvas.SetActive(false);
//    }
//}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public List<PlayerSO> allUpgrades;
    public Button[] upgradeButtons;

    public float upgradeInterval = 5f; // Mỗi 30 giây mở bảng nâng cấp 1 lần

    private PlayerStats playerStats;

    void Start()
    {
        upgradeCanvas.SetActive(false);
        playerStats = FindObjectOfType<PlayerStats>();

        StartCoroutine(TriggerUpgradePeriodically());
    }

    public void ShowUpgrades()
    {
        upgradeCanvas.SetActive(true);

        for (int i = 0; i < upgradeButtons.Length && i < allUpgrades.Count; i++)
        {
            var upgrade = allUpgrades[i]; // lấy đúng thứ tự đã gán trong Inspector

            // Gán tên và ảnh cho nút
            upgradeButtons[i].transform.GetChild(0).GetComponent<Text>().text = upgrade.upgradeName;
            upgradeButtons[i].transform.GetChild(1).GetComponent<Image>().sprite = upgrade.icon;

            // Gán sự kiện cho nút
            upgradeButtons[i].onClick.RemoveAllListeners();
            int index = i; // để tránh lỗi capture biến trong lambda
            upgradeButtons[i].onClick.AddListener(() => ApplyUpgrade(allUpgrades[index]));
        }
    }


    List<PlayerSO> GetRandomUpgrades(int count)
    {
        List<PlayerSO> result = new List<PlayerSO>();
        List<PlayerSO> copy = new List<PlayerSO>(allUpgrades);

        for (int i = 0; i < count && copy.Count > 0; i++)
        {
            int index = Random.Range(0, copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index);
        }

        return result;
    }

    void ApplyUpgrade(PlayerSO upgrade)
    {
        playerStats.ApplyUpgrade(upgrade);
        upgradeCanvas.SetActive(false);
    }

    IEnumerator TriggerUpgradePeriodically()
    {
        while (true)
        {
            Debug.Log("Đợi nâng cấp...");
            yield return new WaitForSeconds(upgradeInterval);

            Debug.Log("Hiển thị nâng cấp!");
            if (!upgradeCanvas.activeInHierarchy)
            {
                ShowUpgrades();
            }
        }
    }

}
