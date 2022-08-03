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
        if (References.allEnemies.Count < 1)
        {
            //wait a bit
            secondsBeforeNextLevel -= Time.deltaTime;

            //stop alarm
            References.alarmManager.StopTheAlarm();

            if (secondsBeforeNextLevel <= 0)
            {
                //then go to the next level
                SceneManager.LoadScene(References.levelGenerator.nextLevelName);
            }
        }
        else
        {
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
