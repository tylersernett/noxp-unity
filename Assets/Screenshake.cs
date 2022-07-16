using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour
{
    Vector3 normalPosition;
    Vector3 desiredPosition;
    public Vector3 joltVector;
    public float shakeAmount;

    public float joltDecayFactor;
    public float shakeDecayFactor;

    public float maxMoveSpeed;

    private void Awake()
    {
        References.screenshake = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shakeVector = new Vector3(GetRandomShakeAmount(), GetRandomShakeAmount(), GetRandomShakeAmount());
        desiredPosition = normalPosition + joltVector + shakeVector;

        //set position to jolted position
        transform.position = Vector3.MoveTowards( transform.position, desiredPosition, maxMoveSpeed* Time.deltaTime);
        joltVector *= joltDecayFactor; //decrease the joltVector each update
        shakeAmount *= shakeDecayFactor;
    }

    float GetRandomShakeAmount()
    {
        return Random.Range(-shakeAmount, shakeAmount);
    }
}
