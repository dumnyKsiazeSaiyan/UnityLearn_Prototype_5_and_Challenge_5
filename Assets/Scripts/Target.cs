using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    private Rigidbody targetRigidbody;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    private int minPowerUp = 1200;
    private int maxPowerUp = 1900;

    private int torquePowerRange = 190000;
    private float xRange = 4.0f;
    private float ySpawnPos = -1.0f;

    public int pointValue;

    void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //fly force
        targetRigidbody.AddForce(VectorUpWithRandomForce(),ForceMode.Impulse);

        //rotation/torque force
        targetRigidbody.AddTorque(VectorWithRandomTorque());

        RandomSpawnPos();
    }


    /*private void OnMouseDown()
    {
        if (gameManager.isGameActive && !gameManager.paused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.UpdateLives(-1);
        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive && !gameManager.paused)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
