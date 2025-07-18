using System.Collections.Generic;
using UnityEngine;

public class StatusUIManager : MonoBehaviour
{
    //instance
    private static StatusUIManager instance;

    public static StatusUIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // spawn UI
    public void SpawnStatusUI(GameObject ownerGameObject, GameObject statusUIPrefab)
    {
        if (ownerGameObject == null || statusUIPrefab == null)
        {
            Debug.LogError("Owner or Status UI is not assigned.");
            return;
        }

        // Instantiate the status UI
        GameObject statusUIInstance = Instantiate(statusUIPrefab, ownerGameObject.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        statusUIInstance.transform.SetParent(ownerGameObject.transform); // Set the owner as the parent
        // destroy the status UI after 2 seconds
        Destroy(statusUIInstance, 0.5f);
    }
}
