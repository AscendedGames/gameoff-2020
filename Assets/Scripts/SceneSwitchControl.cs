using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchControl : MonoBehaviour
{
    private GameObject currentPanel;
    private GameObject MainMenuPanel;
    private GameObject OptionsPanel;
    private GameObject CreditsPanel;

    private LevelLoader levelLoader;

    private void Start()
    {
        OptionsPanel = GameObject.Find("OptionsPanel");
        MainMenuPanel = GameObject.Find("MainMenuPanel");
        CreditsPanel = GameObject.Find("CreditsPanel");
        currentPanel = MainMenuPanel;

        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1f;
        }
    }

    public void BtnQuitToMenu() //Load the Main Menu Scene & Sets timescale to 1
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void BtnMainMenu() //Switch the Panel To Main Menu Panel
    {
        currentPanel.GetComponent<Animator>().Play("Slide-Right-Out");
        MainMenuPanel.GetComponent<Animator>().Play("Slide-Right-In");
        currentPanel = MainMenuPanel;
    }

    public void BtnNewGame() //Load the Game (By the way.)
    {
        levelLoader.TransitionToLevel("Opening");
        Time.timeScale = 1f;
    }

    public void BtnOptions() //Moves the Options Panel into view
    {
        OptionsPanel.GetComponent<Animator>().Play("Slide-Left-In");
        currentPanel.GetComponent<Animator>().Play("Slide-Left-Out");
        currentPanel = OptionsPanel;
    }

    public void BtnCredits() //Access the Credits Menu
    {
        CreditsPanel.GetComponent<Animator>().Play("Slide-Down-In");
        currentPanel.GetComponent<Animator>().Play("Slide-Down-Out");
        currentPanel = CreditsPanel;
    }

    public void BtnQuitGame() //Exit Application
    {
        Application.Quit();
    }
}
