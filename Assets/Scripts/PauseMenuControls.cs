using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PauseMenuControls : MonoBehaviour
{
    private GameObject currentPausePanel;

    public static bool GameIsPaused = false;

    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject pauseOptions;
    [SerializeField]
    private GameObject quitConfirmMenu;
    [SerializeField]
    private GameObject resetConfirmMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentPausePanel = pauseMenuUI;
        pauseMenuUI = GameObject.Find("PauseMenu");
        pauseOptions = GameObject.Find("PauseOptions");
        quitConfirmMenu = GameObject.Find("QuitConfirm");
        resetConfirmMenu = GameObject.Find("ResetConfirm");

        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameOver.activeInHierarchy != true)
        {
            if (GameIsPaused)
            {
                BtnResume();
            }
            else
            {
                BtnPause();
            }
        }
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

        if (currentPausePanel != pauseMenuUI)
        {
            currentPausePanel.GetComponent<CanvasGroup>().alpha = 0;
            currentPausePanel.GetComponent<Animator>().Play("Slide-Left-Out");
            pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-In");

            currentPausePanel = pauseMenuUI;
        }
        else pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-In");

        currentPausePanel.GetComponent<CanvasGroup>().alpha = 1;

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void BtnPauseMenu()
    {
        pauseMenuUI.GetComponent<CanvasGroup>().alpha = 1;
        pauseMenuUI.GetComponent<Animator>().Play("Slide-Left-In");
        currentPausePanel.GetComponent<Animator>().Play("Slide-Left-Out");

        currentPausePanel = pauseMenuUI;
    }

    public void BtnOptionsMenu()
    {
        pauseOptions.GetComponent<CanvasGroup>().alpha = 1;
        pauseOptions.GetComponent<Animator>().Play("Slide-Left-In");
        currentPausePanel.GetComponent<Animator>().Play("Slide-Left-Out");

        currentPausePanel = pauseOptions;
    }

    public void BtnQuitConfirm()
    {
        quitConfirmMenu.GetComponent<CanvasGroup>().alpha = 1;
        quitConfirmMenu.GetComponent<Animator>().Play("Slide-Left-In");
        currentPausePanel.GetComponent<Animator>().Play("Slide-Left-Out");

        currentPausePanel = quitConfirmMenu;
    }

    public void BtnResetConfirm()
    {
        resetConfirmMenu.GetComponent<CanvasGroup>().alpha = 1;
        resetConfirmMenu.GetComponent<Animator>().Play("Slide-Left-In");
        currentPausePanel.GetComponent<Animator>().Play("Slide-Left-Out");

        currentPausePanel = resetConfirmMenu;
    }
}
