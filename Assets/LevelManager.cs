using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool alarmSounded;
    public string firstLevelName;
    public float secondsBeforeNextLevel;
    public float graceTimeAtEndOfLevel;

    public float secondsBeforeShowingDeathMenu;
    bool shownDeathMenu;


    private void Awake()
    {
        References.levelManager = this;
    }

    void Start()
    {
        SceneManager.LoadScene(firstLevelName);

        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        shownDeathMenu = false;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Startup");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if all enemies are dead, 
        if (References.allEnemies.Count < 1 && References.alarmManager.enemiesHaveSpawned)
        {
            //stop alarm
            References.alarmManager.StopTheAlarm();
            if (secondsBeforeNextLevel > 0) //always check this to prevent infinite ShowMainMenu bug [tutorial]
            {
                //wait a bit
                secondsBeforeNextLevel -= Time.deltaTime;

                if (secondsBeforeNextLevel <= 0)
                {
                    if (References.levelGenerator.showMenuWhenDone) //used to end tutorial
                    {
                        References.canvas.ShowMainMenu();
                    }
                    else
                    {
                        //then go to the next level
                        SceneManager.LoadScene(References.levelGenerator.nextLevelName);
                    }
                }
            }
        }
        else
        {
            //not all enemies are dead
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }

        //Player is Dead
        if (References.thePlayer == null && shownDeathMenu == false)
        {
            secondsBeforeShowingDeathMenu -= Time.deltaTime;
            if (secondsBeforeShowingDeathMenu <= 0)
            {
                References.canvas.ShowScoreMenu();
                shownDeathMenu = true;
            }
        }
    }
}
