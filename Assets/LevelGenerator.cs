using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public string nextLevelName;
    public int alarmLevels;

    public List<GameObject> possibleChunkPrefabs;
    public List<GameObject> WeaponPrefabs;
    public GameObject antiquePrefab;
    public GameObject guardPrefab;

    public List<GameObject> swarmerTypes;

    public float fractionOfPlinthsToHaveAntiques;
    public int numberOfGuardsToCreate;
    public int numberOfSpawnersToCreate;

    public int widthInChunks;
    public int lengthInChunks;

    private void Awake()
    {
        References.levelGenerator = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < lengthInChunks; j++)
        {
            for (int i = 0; i < widthInChunks; i++)
            {
                //get a random chunk type
                int randomChunkIndex = Random.Range(0, possibleChunkPrefabs.Count);
                GameObject randomChunkType = possibleChunkPrefabs[randomChunkIndex];
                Vector3 spawnPosition = transform.position + new Vector3(i * 15, 0, j * 30);
                Instantiate(randomChunkType, spawnPosition, Quaternion.identity);
                //.identity: default rotation, analog to vector3.zero
            }
        }

        int numberOfThingsToPlace = References.plinths.Count;
        int numberOfAntiquesToPlace = Mathf.RoundToInt(numberOfThingsToPlace * fractionOfPlinthsToHaveAntiques);
        

        foreach(Plinth plinth in References.plinths)
        {
            GameObject thingToCreate;
            float chanceOfAntique = numberOfAntiquesToPlace / numberOfThingsToPlace;
            if (Random.value <= chanceOfAntique)
            {
                //place an antique 
                thingToCreate = antiquePrefab;
            } else
            {
                //place a weapon
                int randomThingIndex = Random.Range(0, WeaponPrefabs.Count);
                thingToCreate = WeaponPrefabs[randomThingIndex];
            }
            numberOfThingsToPlace--;
            GameObject newThing = Instantiate(thingToCreate);
            plinth.AssignItem(newThing);
        }

        //make sure spawn points are not right by the player
        List<NavPoint> possibleSpots = new List<NavPoint>();
        float minDistanceFromPlayer = 12;
        foreach (NavPoint nav in References.navPoints)
        {
            //is it far enough from the player?
            if (Vector3.Distance(nav.transform.position, References.thePlayer.transform.position) >= minDistanceFromPlayer)
            {
                possibleSpots.Add(nav);
            }
        }

        //create guards
        for (int i = 0; i < numberOfGuardsToCreate; i++)
        {
            if (possibleSpots.Count == 0) { break; }
            int randomIndex = Random.Range(0, possibleSpots.Count);
            NavPoint spotToSpawnAt = possibleSpots[randomIndex];
            Instantiate(guardPrefab, spotToSpawnAt.transform.position, Quaternion.identity);
            possibleSpots.Remove(spotToSpawnAt); //prevent spawning 2 guards at same spot
        }

        //delete a few random spawners
        while (References.spawners.Count > numberOfSpawnersToCreate)
        {
            int randomIndex = Random.Range(0, References.spawners.Count);
            Destroy(References.spawners[randomIndex].gameObject);
        }
        References.alarmManager.SetUpLevel(alarmLevels);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
