using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnPoint;
    public float secondsBetweenSpawns;
    float secondsSinceLastSpawn;

    public int enemiesToSpawn;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastSpawn = secondsBetweenSpawns; //set to 0 if you want a bit of a delay
    }

    void OnEnable()
    {
        References.spawners.Add(this); //add this point to the List
    }


    private void OnDisable()
    {
        References.spawners.Remove(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //fixed update happens the same number of time for all players -- good place for gameplay critical items
    private void FixedUpdate()
    {
        if (References.alarmManager.AlarmHasSounded() && enemiesToSpawn > 0)
        {
            secondsSinceLastSpawn += Time.fixedDeltaTime;
            if (secondsSinceLastSpawn >= secondsBetweenSpawns)
            {
                Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);//Quaternion.Euler(0,0,0)
                secondsSinceLastSpawn = 0;
                enemiesToSpawn--;
            }
        }
    }
}
