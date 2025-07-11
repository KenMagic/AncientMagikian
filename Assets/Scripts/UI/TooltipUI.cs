using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TMP_Text tooltipText;
    public Vector2 offset = new Vector2(10f, -10f);

    public static TooltipUI Instance;

    private void Awake()
    {
        Instance = this;
        HideTooltip();
    }

    public void ShowTooltip(string content)
    {
        tooltipText.text = content;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                tooltipPanel.transform.parent.GetComponent<RectTransform>(),
                Input.mousePosition,
                null,
                out pos
            );
            tooltipPanel.GetComponent<RectTransform>().anchoredPosition = pos + offset;
        }
    }
}
