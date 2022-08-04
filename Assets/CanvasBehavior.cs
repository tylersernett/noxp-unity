using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasBehavior : MonoBehaviour
{
    public SceneAsset firstScene;
    public GameObject mainMenu;
    public GameObject currentMenu;
    public GameObject creditsMenu;
    public GameObject scoreMenu;
    
    public GameObject usePrompt;
    public bool usePromptSignal;

    public Transform gameUIParent;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI recentScoreText;
    public TextMeshProUGUI alarmCountdownText;

    public WeaponPanel mainWeaponPanel;
    public WeaponPanel secondaryWeaponPanel;



    // Awake happens before Start()
    void Awake()
    {
        //initiate references in awake.
        References.canvas = this;
        currentMenu = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (currentMenu != null)
            {
                HideMenu();
            }
            else
            {
                ShowMenu(mainMenu);
            }
        }

        usePrompt.SetActive(usePromptSignal);
        usePromptSignal = false;
    }

    public void ShowMainMenu()
    {
        ShowMenu(mainMenu);
    }

    public void ShowScoreMenu()
    {
        ShowMenu(scoreMenu);
    }

    public void HideMenu()
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
        }
        currentMenu = null;
        Time.timeScale = 1;
    }

    //public void StartNewGame()
    //{
    //    HideMenu();
    //    SceneManager.LoadScene(firstScene.name);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowMenu(GameObject menuToShow)
    {
        HideMenu();
        currentMenu = menuToShow;
        if (menuToShow != null)
        {
            menuToShow.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
