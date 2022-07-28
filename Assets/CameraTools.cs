using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    Vector3 normalPosition;
    Vector3 desiredPosition;
    public Vector3 joltVector;
    public float shakeAmount;

    public float joltDecayFactor;
    public float shakeDecayFactor;

    public float maxMoveSpeed;

    public Vector3 cameraOffset;

    private void Awake()
    {
        References.cameraTools = this;
    }

    void Start()
    {
        normalPosition = transform.position;
        //store position relative to player
        cameraOffset = transform.position - References.thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (References.thePlayer != null)
        {
            //set position by lookint @ player's pos & adding offset
            normalPosition = References.thePlayer.transform.position + cameraOffset;
        }

        Vector3 shakeVector = new Vector3(GetRandomShakeAmount(), GetRandomShakeAmount(), GetRandomShakeAmount());
        desiredPosition = normalPosition + joltVector + shakeVector;

        //set position to jolted position
        transform.position = Vector3.MoveTowards( transform.position, desiredPosition, maxMoveSpeed* Time.deltaTime);

        //decrease jolt/shake
        joltVector *= joltDecayFactor; //decrease the joltVector each update
        shakeAmount *= shakeDecayFactor;
    }

    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
