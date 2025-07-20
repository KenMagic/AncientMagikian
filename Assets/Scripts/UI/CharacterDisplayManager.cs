using UnityEngine;
using System.Collections.Generic;

public class CharacterSelectManager : MonoBehaviour
{
    public List<PlayerStatsSO> availableCharacters;
    public GameObject thumbnailPrefab;
    public Transform thumbnailParent;
    public CharacterDetailDisplay detailDisplay;

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
        GameController.Instance.currentPlayer = stats;
    }
}
