using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false; //make them invisible to player
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        References.navPoints.Add(this); //add this point to the List
    }


    private void OnDisable()
    {
        References.navPoints.Remove(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
