using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed = 1.1f;
    public Rigidbody objectRb;
    private float zDestroy = -7.2f;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.forward * -speed);

        //new
        if (gameObject.CompareTag("Enemy"))
        {
            objectRb.transform.Rotate(2, 2, 2);
        }
        //new



        //destroy prefabs
        Bottom();
    }


    void Bottom()
    {
        if (transform.position.z < zDestroy)
        {
            Destroy(gameObject);
        }
    }

}
