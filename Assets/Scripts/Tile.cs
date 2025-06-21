using UnityEngine;

public class Tile : MonoBehaviour
{

    public GameObject tileAura;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the tile aura to be inactive by default
        if (tileAura != null)
        {
            tileAura.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to activate the tile aura
    public void ActivateAura()
    {
        if (tileAura != null)
        {
            tileAura.SetActive(true);
        }
    }

    // Method to deactivate the tile aura
    public void DeactivateAura()
    {
        if (tileAura != null)
        {
            tileAura.SetActive(false);
        }
    }
    
}
