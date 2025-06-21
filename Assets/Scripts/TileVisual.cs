using UnityEngine;

public class TileVisual : MonoBehaviour
{
    public SpriteRenderer grassRenderer;
    public SpriteRenderer dirtRenderer;

    private void Awake()
    {
        ShowGrass(); // mặc định là grass
    }

    public void ShowGrass()
    {
        grassRenderer.enabled = true;
        dirtRenderer.enabled = false;
    }

    public void ShowDirt()
    {
        grassRenderer.enabled = false;
        dirtRenderer.enabled = true;
    }
}
