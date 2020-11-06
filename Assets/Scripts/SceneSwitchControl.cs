using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void BtnNewGame()
    {
        SceneManager.LoadScene("Prototyping Scene");
    }

    public void BtnQuitGame()
    {
        Debug.Log("It quit.?");
        Application.Quit();
    }
}
