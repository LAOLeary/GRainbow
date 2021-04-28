using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosions : MonoBehaviour
{
    public AudioSource boombox;
    public AudioClip greenBoom;
    public AudioClip yellowBoom;





    // Start is called before the first frame update
    void Start()
    {
        boombox = GetComponent<AudioSource>();
        boombox.PlayOneShot(greenBoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GreenBoom()
    {
        
    }

    void YellowBoom()
    {
        boombox.PlayOneShot(yellowBoom);
    }

}
