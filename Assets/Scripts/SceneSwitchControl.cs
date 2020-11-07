using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchControl : MonoBehaviour
{
    public void BtnNewGame() //Load the Game
    {
        SceneManager.LoadScene("Prototyping Scene");
    }

    public void BtnMainMenu() //Load the Main Menu
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BtnOptions() //Access the Options Menu
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void BtnCredits() //Access the Credits Menu
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void BtnQuitGame() //Exit Application
    {
        Application.Quit();
    }
}
