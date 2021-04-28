using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPath : MonoBehaviour
{

    public Rigidbody lazerRb;
    public GameObject banana;
    private GameManager gameManager;
    private AudioSource boombox;


    public float speed = 1.0f;
    public float hbound = 12.2f;




    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("SpawnManager").GetComponent<GameManager>();
        boombox = gameManager.GetComponent<AudioSource>();
        gameManager.Pewpew();

        lazerRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        lazerRb.AddForce(Vector3.left * -speed);

        if (lazerRb.position.x > hbound)
        {
            Destroy(gameObject);
        }
    }

        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            Destroy(collision.gameObject);
        }
    }



}



