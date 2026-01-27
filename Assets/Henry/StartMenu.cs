using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

}