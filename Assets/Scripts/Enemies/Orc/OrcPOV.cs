using UnityEngine;

public class OrcPOV : MonoBehaviour
{
    private Orc orc;

    private void Awake()
    {
        orc = GetComponentInParent<Orc>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (orc == null) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("OrcPOV: Phát hiện Player");
            orc.StartChasing(other.transform);
        }
        else if (other.CompareTag("Tower"))
        {
            Debug.Log("OrcPOV: Phát hiện Tower");
            orc.TryAttackTower(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (orc == null) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("OrcPOV: Player rời vùng");
            orc.StopChasingAfterDelay(); // sẽ delay 2s trong Orc.cs
        }
    }
}