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
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //x, y, z
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();

        ourRigidBody.velocity = inputVector * speed;

        //face new position
        Vector3 lookAtPosition = transform.position + inputVector; //just getting direction from this
        transform.LookAt(lookAtPosition);//change facing rotation
      
        //if clicked, create bullet at our current position
        if (Input.GetButton("Fire1")) {
            Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation); //transform.forward = 1 unit in forward direction
        }
    }
}
