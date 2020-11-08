using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class SceneSwitchControl : MonoBehaviour
{
    private GameObject MainMenuPanel;
    private GameObject OptionsPanel;
    private GameObject CreditsPanel;
    public void BtnNewGame() //Load the Game
    {
        SceneManager.LoadScene("Prototyping Scene");
    }

    public void BtnMainMenu() //Load the Main Menu
    {
        OptionsPanel = GameObject.Find("OptionsPanel");
        MainMenuPanel = GameObject.Find("MainMenuPanel");
        CreditsPanel = GameObject.Find("CreditsPanel");
        OptionsPanel.GetComponent<Animator>().Play("Slide-Right-Out");
        CreditsPanel.GetComponent<Animator>().Play("Slide-Right-Out");
        MainMenuPanel.GetComponent<Animator>().Play("Slide-Right-In");
    }

    public void BtnOptions() //Access the Options Menu
    {
        OptionsPanel = GameObject.Find("OptionsPanel");
        MainMenuPanel = GameObject.Find("MainMenuPanel");
        OptionsPanel.GetComponent<Animator>().Play("Slide-Left-In");
        MainMenuPanel.GetComponent<Animator>().Play("Slide-Left-Out");
    }

    public void BtnCredits() //Access the Credits Menu
    {
        CreditsPanel = GameObject.Find("CreditsPanel");
        MainMenuPanel = GameObject.Find("MainMenuPanel");
        CreditsPanel.GetComponent<Animator>().Play("Slide-Left-In");
        MainMenuPanel.GetComponent<Animator>().Play("Slide-Left-Out");
    }

    public void BtnQuitGame() //Exit Application
    {
        Application.Quit();
    }
}
