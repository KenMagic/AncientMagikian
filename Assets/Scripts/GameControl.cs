using UnityEngine;
using UnityEngine.Tilemaps;

public class GameControl : MonoBehaviour
{

    private GameObject selectedTile; // Reference to the currently selected tile

    public Tilemap  tilemap; // Reference to the Tilemap component

    public TileBase highlightTile; // Reference to the tile used for highlighting

    public TileBase defaultTile; // Reference to the default tile

    public Vector3Int selectedTileBase; // Reference to the tile used for selection
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if (selectedTile != null)
            {
                selectedTile.GetComponent<Tile>().DeactivateAura(); // Activate the aura of the previously selected tile
            }
            Debug.Log("Mouse clicked");
            SelectTile();
        }
        HighlightTile(); // Highlight the tile under the mouse cursor
    }

    //select a tile
    public Vector2Int SelectTile()
    {
        // Get mouse position in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Raycast to check if mouse is over a tile object (with "Tile" tag)
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Tile"))
        {
            hit.collider.GetComponent<Tile>().ActivateAura(); // Activate the tile aura
            selectedTile = hit.collider.gameObject; // Store the selected tile reference
            // Convert hit position to tile coordinates
            Vector2Int tilePosition = new Vector2Int(
            Mathf.FloorToInt(hit.point.x),
            Mathf.FloorToInt(hit.point.y)
            );
            Debug.Log($"Selected tile at position: {tilePosition}");
            return tilePosition;
        }

        // Not hovering over a tile object
        Debug.Log("No tile selected");
        return Vector2Int.zero;
    }
    // check hover tile over tilemap
    void HighlightTile()
    {
        Vector3 mouseTilePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilemapPosition = tilemap.WorldToCell(mouseTilePosition);
        Vector3Int tilemapPositionOffset = new Vector3Int(tilemapPosition.x, tilemapPosition.y, 0);
        Vector3Int hoveredTilePosition = tilemap.WorldToCell(mouseTilePosition);
        
        if (tilemap.GetTile(hoveredTilePosition) == null)
        {
            if (tilemap.GetTile(selectedTileBase) != null)
            {
                tilemap.SetTile(selectedTileBase, defaultTile);
            }    // Reset the tile to default if it's not the selected tile
            return; // If the hovered tile is not a valid tile, do nothing
        }
        if (selectedTileBase != hoveredTilePosition)
        {
            if (tilemap.GetTile(selectedTileBase) != null)
            {
                tilemap.SetTile(selectedTileBase, defaultTile);
            } 
        }
        tilemap.SetTile(hoveredTilePosition, highlightTile);
        selectedTileBase = hoveredTilePosition;
         // Store the hovered tile's position
    }
}
