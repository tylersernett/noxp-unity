using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool alarmSounded;
    public SceneAsset nextLevel;

    private void Awake()
    {
        References.levelManager = this;
    }

    void Start()
    {
        alarmSounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if all enemies are dead, 
        if (References.allEnemies.Count < 1)
        {
            //go to the next level
            SceneManager.LoadScene(nextLevel.name);
        }
    }
}
