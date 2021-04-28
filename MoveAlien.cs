using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlien : MonoBehaviour
{
    public float speed = 100.0f;
    public float moveDirection;
    public float trajectory;

    public Rigidbody objectRb;
    public GameObject alien;
    public GameObject lazer;


    public bool stalled;
    public float zbound = 6.0f;



    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        alien = GameObject.Find("Alien");
        lazer = GameObject.Find("Lazer");

    }

    // Update is called once per frame
    void Update()
    {
        //move program for alien
        moveDirection = Random.Range(-5, 5);
        InvokeRepeating("Alienmove", 1f, 6f);
        Boundaries();

    }


    void Boundaries()
    {
        if (objectRb.position.z > zbound)
        {
            objectRb.Sleep();
            objectRb.transform.position = new Vector3(transform.position.x, transform.position.y, zbound - 0.3f);

            //objectRb.AddForce(Vector3.back * moveDirection * speed);
        }

        if (objectRb.position.z < -zbound)
        {
            objectRb.Sleep();
            objectRb.transform.position = new Vector3(transform.position.x, transform.position.y, 0.3f - zbound);
            //objectRb.AddForce(Vector3.back * moveDirection * speed);
        }
    }


        void Alienmove()
    {
        if (stalled == false)
        {
            moveDirection = Random.Range(-4, 4);
            trajectory = speed * moveDirection;
            objectRb.AddForce(Vector3.back * moveDirection * speed);
            StartCoroutine(Pause());
        }
    }


    IEnumerator Pause()
    {
        stalled = true;
        yield return new WaitForSeconds(1f);
        objectRb.Sleep();

       

        yield return new WaitForSeconds(1f);
        objectRb.WakeUp();
        stalled = false;
    }

    



}
