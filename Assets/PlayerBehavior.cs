using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float speed; //public: things outside PlayerBehavior will access the variable, access in inspector
                        //inspector always OVERRIDES values 
                        //so DELCARE here, then TWEAK in inspector

    public List<WeaponBehavior> weapons = new List<WeaponBehavior>(); //List<ScriptName> varName = new List<ScriptName>();
    public int selectedWeaponIndex;



    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer = this;
        selectedWeaponIndex = 0;
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
        Plane playerPlane = new Plane(Vector3.up, transform.position); //create plane same height as player. InNorml - up: vector perpendicular to plane
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera); //ouput a ray of this distance
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera); //then get coord's of that ray
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);//face new position

        //FIRING
        
        if (weapons.Count > 0 && Input.GetButton("Fire1"))
        {
            //tell weapon to fire
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }

        //change weapon
        if (weapons.Count > 0 && Input.GetButtonDown("Fire2"))
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
        }

        //use item
        if (Input.GetButtonDown("Use"))
        {
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
                nearestUseable.Use();
            }
        }

        
    }

    public void SelectLatestWeapon()
    {
        ChangeWeaponIndex(weapons.Count - 1);
    }

    private void ChangeWeaponIndex(int index)
    {
        //selectedWeaponIndex += 1;
        selectedWeaponIndex = index;
        selectedWeaponIndex = selectedWeaponIndex % weapons.Count; //in unity, % is remainder, NOT modulus...so find work around

        for (int i = 0; i < weapons.Count ; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
            } else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
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
