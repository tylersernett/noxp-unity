using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer.transform.position = transform.position;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
