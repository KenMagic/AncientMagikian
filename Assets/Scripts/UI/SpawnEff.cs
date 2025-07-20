using UnityEngine;

public class SpawnEff : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer character;

    void Start()
    {
        character.sprite = GameController.Instance.CurrentPlayer.portrait;

        float scaleFactor = 100f/16f;
        character.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
