using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed; //public: things outside PlayerBehavior will access the variable, access in inspector
                        //inspector always OVERRIDES values 
                        //so DELCARE here, then TWEAK in inspector

    public WeaponBehavior mainWeapon;
    public WeaponBehavior secondaryWeapon;

    // Start is called before the first frame update
    private void Awake()
    {
        References.thePlayer = this;
    }
    private void Start()
    {
        //SetAsMainWeapon(mainWeapon);

        if (mainWeapon != null)
        {
            //mainWeapon.GetComponent<Useable>().Use();
            References.canvas.mainWeaponPanel.AssignWeapon(mainWeapon);
        }
    }




    // Update is called once per frame
    void Update()
    {

        //MOVEMENT: WASD
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //x, y, z
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = inputVector * speed;

        //cursor detection
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);//create ray from 'eyes' to cursor position
        Plane playerPlane = new Plane(Vector3.up, transform.position); //create plane same height as player. InNorml - up: vector perpendicular to plane
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera); //ouput a ray of this distance
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera); //then get coord's of that ray
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);//face new position

        //FIRING

        if (mainWeapon != null && Input.GetButton("Fire1"))
        {
            //tell weapon to fire
            mainWeapon.Fire(cursorPosition);
        }

        //change weapon
        if (Input.GetButtonDown("Fire2"))
        {
            SwitchWeapons();
        }

        //use item

        //use the NEAREST usable
        Useable nearestUseable = null;
        float nearestDistance = 3; //maximum pickup distance 
        foreach (Useable thisUseable in References.useables)
        {
            float thisDistance = Vector3.Distance(transform.position, thisUseable.transform.position);
            if (thisDistance <= nearestDistance)
            {
                nearestUseable = thisUseable;
                nearestDistance = thisDistance;
            }
        }
        if (nearestUseable != null)
        {
            //if something is in used range, and hasn't been used yet, show use prompt
            References.canvas.usePromptSignal = true;
            if (Input.GetButtonDown("Use"))
            {
                //use it up when button pressed
                nearestUseable.Use();
            }
        }

    }

    public void PickUpWeapon(WeaponBehavior weapon)
    {
        if (mainWeapon == null)
        {
            SetAsMainWeapon(weapon);
        }
        else
        {
            //if we already have a main weapon
            if (secondaryWeapon == null)
            {
                SetAsSecondaryWeapon(weapon);
            }
            else
            {
                //both slots already full, then drop weapon currently being held, keep 2ndary one in 'backpack'
                //drop old main
                mainWeapon.Drop();

                //set new main
                SetAsMainWeapon(weapon);
            }
        }
    }

    void SetAsMainWeapon(WeaponBehavior weapon)
    {
        mainWeapon = weapon;
        References.canvas.mainWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(true);
    }

    void SetAsSecondaryWeapon(WeaponBehavior weapon)
    {
        secondaryWeapon = weapon;
        References.canvas.secondaryWeaponPanel.AssignWeapon(weapon);
        weapon.gameObject.SetActive(false);
    }

    private void SwitchWeapons()
    {
        if (mainWeapon != null && secondaryWeapon != null)
        {
            WeaponBehavior oldMainWeapon = mainWeapon;
            SetAsMainWeapon(secondaryWeapon);
            SetAsSecondaryWeapon(oldMainWeapon);
        }
    }

    private void OnDestroy()
    {
        References.scoreManager.UpdateHighScore();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        WeaponBehavior theirWeapon = other.GetComponentInParent<WeaponBehavior>(); //check if parent has WeaponBehavior
        if (theirWeapon != null)
        {
            theirWeapon.BePickedUpByPlayer();
        }
    }*/
}
