using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("MapScenceOld"); 
    }

    public void OnSettingButtonClick()
    {
        Debug.Log("Setting button clicked"); 
    }
}
