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

        //r�cup�rer automatiquement le rigidbody
        targetrb = GetComponent<Rigidbody>();
        targetrb.AddForce (RandomForce(), ForceMode.Impulse);
        //faire tourner de mani�re random
        targetrb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        //pour �viter de spawn toujours au m�me endroit, on va random la position du gameobject au start
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

    //quand la souris entre en contact avec un objet, il est d�truit et joue un son. Si son tag est "Bad", on lance la fonction gameover, si son tag n'est pas "Bad", on ajoute des points au score.
    private void OnMouseEnter()
    {
        if (gameManager.isGameActive)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            PlayDestroySound();
            Destroy(gameObject);
           
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateScore(pointValue);
            }
            else 
            {
                gameManager.GameOver();
            }
        }
    }

    //d�truit les objets non touch�s qui tombent hors de l'�cran.
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    void DestroyOnTime()
    {
        Destroy(gameObject);
    }

    void PlayDestroySound()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("bruit");
    }
    
}
