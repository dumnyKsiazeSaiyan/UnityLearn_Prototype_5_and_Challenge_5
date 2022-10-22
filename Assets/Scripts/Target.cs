using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRigidbody;

    private int minPowerUp = 1200;
    private int maxPowerUp = 1900;

    private int torquePowerRange = 190000;
    private float xRange = 4.0f;
    private float ySpawnPos = -1.0f;

    void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();

        //fly force
        targetRigidbody.AddForce(VectorUpWithRandomForce(),ForceMode.Impulse);

        //rotation/torque force
        targetRigidbody.AddTorque(VectorWithRandomTorque());

        RandomSpawnPos();
    }

    private void Update()
    {

    }


    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }




    Vector3 VectorUpWithRandomForce()
    {
        int randomForce = Random.Range(minPowerUp, maxPowerUp);
        Vector3 randomForceVector = randomForce * Time.deltaTime * Vector3.up;
        return randomForceVector;
    }

    Vector3 VectorWithRandomTorque()
    {
        float randomTorque = Random.Range(-torquePowerRange, torquePowerRange) * Time.deltaTime;
        Vector3 randomTorqueVector = new(randomTorque, randomTorque, randomTorque);
        return randomTorqueVector;
    }

    void RandomSpawnPos()
    {
        transform.position = new(Random.Range(-xRange, xRange), ySpawnPos);

    }

}
