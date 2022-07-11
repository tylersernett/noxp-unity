using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBehavior : MonoBehaviour
{
    // Awake happens before Start()
    void Awake()
    {
        //initiate references in awake.
        References.canvas = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
