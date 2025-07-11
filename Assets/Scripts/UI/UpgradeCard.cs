using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    public string tooltipContent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipUI.Instance.ShowTooltip(tooltipContent);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipUI.Instance.HideTooltip();
    }
}
