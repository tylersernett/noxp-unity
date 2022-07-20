using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool alarmSounded;
    public SceneAsset nextLevel;
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
        alarmSounded = false;
        secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        shownDeathMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if all enemies are dead, 
        if (References.allEnemies.Count < 1)
        {
            //wait a bit
            secondsBeforeNextLevel -= Time.deltaTime;

            if (secondsBeforeNextLevel <= 0)
            {
                //then go to the next level
                SceneManager.LoadScene(nextLevel.name);
            }
        }
        else
        {
            secondsBeforeNextLevel = graceTimeAtEndOfLevel;
        }

        if (References.thePlayer == null && shownDeathMenu == false)
        {
            secondsBeforeShowingDeathMenu -= Time.deltaTime;
            if (secondsBeforeShowingDeathMenu <= 0)
            {
                References.canvas.ShowMainMenu();
                shownDeathMenu = true;
            }
        }
    }
}
