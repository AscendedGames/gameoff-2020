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

    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseOptions;
    [SerializeField] private GameObject quitConfirmMenu;
    [SerializeField] private GameObject resetConfirmMenu;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
               BtnResume();
            } else
            {
                BtnPause();
            }
        }
    }

    public void BtnNewGame() //Load the Game
    {
        SceneManager.LoadScene("Prototyping Scene");
        Time.timeScale = 1f;
    }

    public void BtnResume()
    {
        pauseOptions.SetActive(false);
        pauseMenuUI.SetActive(false);
        quitConfirmMenu.SetActive(false);
        resetConfirmMenu.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void BtnPause()
    {
        pauseOptions.SetActive(true);
        pauseMenuUI.SetActive(true);
        quitConfirmMenu.SetActive(true);
        resetConfirmMenu.SetActive(true);

        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseOptions = GameObject.Find("PauseOptions");
        pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-In");
        pauseOptions.GetComponent<Animator>().Play("Slide-Left-Out");
        resetConfirmMenu.GetComponent<Animator>().Play("Slide-Left-Out");

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void BtnPauseMenu()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseOptions = GameObject.Find("PauseOptions");
        quitConfirmMenu = GameObject.Find("QuitConfirm");
        resetConfirmMenu = GameObject.Find("ResetConfirm");

        pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-In");
        pauseOptions.GetComponent<Animator>().Play("Slide-Left-Out");
        quitConfirmMenu.GetComponent<Animator>().Play("Slide-Left-Out");
        resetConfirmMenu.GetComponent<Animator>().Play("Slide-Left-Out");
    }

    public void BtnOptionsMenu()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseOptions = GameObject.Find("PauseOptions");

        pauseMenuUI.GetComponent<Animator>().Play("Slide-Right-Out");
        pauseOptions.GetComponent<Animator>().Play("Slide-Right-In");
    }

    public void BtnQuitConfirm()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseOptions = GameObject.Find("PauseOptions");
        quitConfirmMenu = GameObject.Find("QuitConfirm");

        pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-Out");
        pauseOptions.GetComponent<Animator>().Play("Slide-Left-Out");
        quitConfirmMenu.GetComponent<Animator>().Play("Slide-Left-In");
    }

    public void BtnResetConfirm()
    {
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseOptions = GameObject.Find("PauseOptions");
        resetConfirmMenu = GameObject.Find("ResetConfirm");

        pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-Out");
        pauseOptions.GetComponent<Animator>().Play("Slide-Left-Out");
        resetConfirmMenu.GetComponent<Animator>().Play("Slide-Left-In");
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
