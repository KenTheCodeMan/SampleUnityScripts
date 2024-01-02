using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb; 
    private float minSpeed = 10;
    private float maxSpeed = 14;
    private float maxTorque = 10;
    private float xRange = 8;
    private float ySpawnPos = -2;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;
    public AudioClip boxDeath; //audio clip for boxs when they are destroyed

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    Vector3 RandomForce() //random force added between min and max variables for speed
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque() //creates random torque value between positive and negative torque values variable
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()//creates randoms spawn position for a vector3
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    private void OnMouseDown() //destroys game object on mouse click
    {
        if (gameManager.isGameActive && !gameManager.isGamePaused)
        {
        AudioSource.PlayClipAtPoint(boxDeath, transform.position); //box death sound, PlayClipAtPoint is super cool!
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }
    private void OnTriggerEnter(Collider other) //destroys objects that arent destroyed by player
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad")) //if any object that isnt tagged bad crosses the line, game is over. 
        {
            gameManager.GameOver();
        }
    }
}
