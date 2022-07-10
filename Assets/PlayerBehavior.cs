using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed; //public: things outside PlayerBehavior will access the variable, access in inspector
                        //inspector always OVERRIDES values 
                        //so DELCARE here, then TWEAK in inspector

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Input.GetAxis("Vertical"));

        //MOVEMENT: WASD
        float maxDistanceToMove = speed * Time.deltaTime; //time difference
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ); //x, y, z
        Vector3 movementVector = inputVector * maxDistanceToMove; //scale up pieces of inputVector by maxDistanceToMove
        Vector3 newPosition = transform.position + movementVector;

        transform.LookAt(newPosition);//change facing rotation
        transform.position = newPosition;
      
        //click to fire
        //if clicked, create bullet at our current position
        if (Input.GetButton("Fire1")) {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
}
