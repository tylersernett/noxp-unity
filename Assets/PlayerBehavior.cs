using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Input.GetAxis("Vertical"));

        float speed = 10;
        float maxDistanceToMove = speed * Time.deltaTime; //time difference
        //move with W & S
        transform.position += Vector3.forward * Input.GetAxis("Vertical") * maxDistanceToMove;
        //move with A & D
        transform.position += Vector3.right * Input.GetAxis("Horizontal") * maxDistanceToMove;

    }
}
