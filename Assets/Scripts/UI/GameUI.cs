using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Image XPFill;
    [SerializeField]
    private TextMeshProUGUI XP;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateXPBar(float currentHealth, float maxHealth)
    {
        if (XPFill != null)
        {
            float fillAmount = currentHealth / maxHealth;
            XPFill.fillAmount = fillAmount;
            XP.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        }
        else
        {
            Debug.LogError("Fill Image or Health Text is not assigned.");
        }
    }
}
