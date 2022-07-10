using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed; //public: things outside PlayerBehavior will access the variable, access in inspector
                        //inspector always OVERRIDES values 
                        //so DELCARE here, then TWEAK in inspector

    public GameObject bulletPrefab;

    public float secondsBetweenShots;
    float secondsSinceLastShot; //DEFINE here

    // Start is called before the first frame update
    void Start()
    {
        secondsSinceLastShot = secondsBetweenShots; //INITIALIZE here 
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Input.GetAxis("Vertical"));

        //MOVEMENT: WASD
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //x, y, z
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        //cursor detection
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);//create ray from 'eyes' to cursor position
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera); //ouput a ray of this distance
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera); //then get coord's of that ray
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);//face new position

        //FIRING
        secondsSinceLastShot += Time.deltaTime;
        //if clicked, create bullet at our current position
        if (secondsSinceLastShot >= secondsBetweenShots && Input.GetButton("Fire1"))
        {
            Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation); //transform.forward = 1 unit in forward direction
            secondsSinceLastShot = 0;
        }
    }
}
