using UnityEngine;
using UnityEngine.UI;

public class CharacterImageButton : MonoBehaviour
{
    public Image characterImage;
    private PlayerStatsSO stats;
    private System.Action<PlayerStatsSO> onClickCallback;

    public void Setup(PlayerStatsSO stats, System.Action<PlayerStatsSO> onClick)
    {
        this.stats = stats;
        this.onClickCallback = onClick;
        characterImage.sprite = stats.portrait;
        GetComponent<Button>().onClick.AddListener(() => onClickCallback?.Invoke(stats));
    }
}
