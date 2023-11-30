using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //variable rigidbody
    private Rigidbody targetrb;
    //force d'impulsion
    private float minImpulse = 12;
    private float maxImpulse = 16;
    //Rotation
    private float maxTorque = 10;
    //Position
    private float maxSpawnX = 4;
    private float PointSpawnY = -1;

    private float lifetime = 4;

    private Controller gameManager;

    public int pointValue;

    public ParticleSystem explosionParticle;

 

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyOnTime", lifetime);

        //récupérer automatiquement le rigidbody
        targetrb = GetComponent<Rigidbody>();
        targetrb.AddForce (RandomForce(), ForceMode.Impulse);
        //faire tourner de manière random
        targetrb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        //pour éviter de spawn toujours au même endroit, on va random la position du gameobject au start
        transform.position = new Vector3(Random.Range(-4, 4), -1);
        gameManager = GameObject.Find("GameController").GetComponent<Controller>();
        
    }

    Vector3 RandomForce() 
    {
        return Vector3.up * Random.Range(minImpulse, maxImpulse);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawn()
    {
        return new Vector3(Random.Range(-maxSpawnX, maxSpawnX), PointSpawnY);
    }

    private void OnMouseEnter()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Debug.Log("destroyed");
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    void DestroyOnTime()
    {
        Destroy(gameObject);
    }
    
}
