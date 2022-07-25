using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public List<GameObject> possibleChunkPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            //get a random chunk type
            int randomChunkIndex = Random.Range(0, possibleChunkPrefabs.Count);
            GameObject randomChunkType = possibleChunkPrefabs[randomChunkIndex];
            Vector3 spawnPosition = transform.position + new Vector3(i * 15, 0, 0);
            Instantiate(randomChunkType, spawnPosition, Quaternion.identity);
            //.identity: default rotation, analog to vector3.zero
            possibleChunkPrefabs.Remove(randomChunkType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
