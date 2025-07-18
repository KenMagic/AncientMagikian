using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarUI; // Reference to the health bar UI GameObject

    public Image fillImage; // Reference to the fill image of the health bar

    void Start()
    {
        if (healthBarUI == null)
        {
            Debug.LogError("HealthBar UI is not assigned in the inspector.");
            return;
        }
        if (fillImage == null)
        {
            Debug.LogError("Fill Image is not found in the HealthBar UI.");
        }
    }



    //UPDATE HEALTH BAR
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (fillImage != null)
        {
            float fillAmount = currentHealth / maxHealth;
            fillImage.fillAmount = fillAmount;
        }
        else
        {
            Debug.LogError("Fill Image or Health Text is not assigned.");
        }
    }
}
