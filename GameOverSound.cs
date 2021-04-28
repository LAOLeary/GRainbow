using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSound : MonoBehaviour
{

    public AudioSource boombox;

    public PlayerController playerController;


    public AudioClip audioClip;
    public float startingPitch = 1.4f;
    public float endPitch = 1.0f;



    // Start is called before the first frame update
    void Start()
    {
        boombox.pitch = startingPitch;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isAlive == false)
        {
            boombox.pitch = endPitch;
        }

        if (playerController.isAlive == true)
        {
            boombox.pitch = startingPitch;
        }


    }
}
