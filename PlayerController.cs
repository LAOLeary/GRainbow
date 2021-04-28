using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vspeed = 5f;
    public float hspeed = 8f;
    public GameObject player;
    private Rigidbody playerRb;
    private float zAngle;
    public float vBound = 6.3f;
    public float hBound = 11f;
    public float bounce = 5f;

    public float boostspeed;
    public float mass;
    public float drag;
    public float dragboost;
    public float boostwait;

    public bool hasBoost;
    public bool canBoost;
    public bool isAlive;

    public ParticleSystem green;
    public ParticleSystem yellow;
    public ParticleSystem banz;
    
    private GameManager gameManager;
    private SpawnManager spawnManager;

    ///explosions?

    public AudioSource boombox;
    public AudioClip greenBoom;
    public AudioClip yellowBoom;
    public AudioClip bananaChomp;

    public GameObject banana;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        vspeed = 5.0f;
        hspeed = 8.0f;
        boostspeed = 9f;
        mass = 0.42f;
        drag = 2.8f;
        dragboost = 4.0f;

        boostwait = 0.5f;

        playerRb.mass = mass;
        playerRb.drag = drag;

    //boost functions here
    player = GameObject.Find("Player");
        canBoost = true;
        isAlive = true;

        //did this for explosions
        gameManager = GameObject.Find("SpawnManager").GetComponent<GameManager>();
        boombox = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

        Dragin();

        //input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //force
        playerRb.AddForce(Vector3.forward * vspeed * verticalInput);
        playerRb.AddForce(Vector3.right * hspeed * horizontalInput);

        //torque
        playerRb.transform.Rotate(0, 0, 20 * verticalInput);
        playerRb.transform.Rotate(0, 0, 20 * horizontalInput);
        playerRb.transform.Rotate(0, 0, 10);

        //methods
        smoothLookTowardDirectionOfMovement();
        Boundaries();

        //Boost method start//
        if (Input.GetKeyDown(KeyCode.Space) && hasBoost == false && canBoost == true)
        {
            hasBoost = true;
            gameManager.Boost();

            drag *= dragboost;
            playerRb.drag = drag;

            vspeed *= boostspeed;
            hspeed *= boostspeed;
            StartCoroutine(Boostcooldown());
        }

        IEnumerator Boostcooldown()
        {
            Debug.Log("Boost ON!");
            yield return new WaitForSeconds(0.03f);
            hasBoost = false;
            canBoost = false;

            drag /= dragboost;
            playerRb.drag = drag;

            vspeed /= boostspeed;
            hspeed /= boostspeed;
            Debug.Log("Boost OFF!");
            yield return new WaitForSeconds(boostwait);
            canBoost = true;
        }
        //Boost method end//
    }



    /// <summary>
    /// TIME DECAY, so boost gets weaker over time if you don't eat bananas. If you eat them fast enough you overcome the delay.
    /// 
    /// I also put a decrease in boost latency throughout the game too for each banana eaten. 
    /// 
    /// Therefore: boost intensity proportional to hastiness, boost frequency inversely propotional to total score.
    /// </summary>

    //timedecay
    void Dragin()
    {
        if (boostspeed > 5.0f)
       {
            boostspeed -= (Time.deltaTime * 0.04f);
        }

        if (boostwait < 7.0f)
        {
            boostwait += (Time.deltaTime * 0.1f);
        }
                


    }







    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //explosion
            yellow.transform.position = gameObject.transform.position;
            yellow.Play();

            //sound
            gameManager.YellowBoom();

            Debug.Log("yellow banana!");
            Destroy(gameObject);
            
            //gamecondition
            isAlive = false;
            gameManager.GameOver1();
        }

        if (other.CompareTag("Lazer"))
        {
            green.transform.position = gameObject.transform.position;
            green.Play();
            gameManager.GreenBoom();
            Debug.Log("green lazer!");
            Destroy(gameObject);
            //gamecondition
            isAlive = false;
            gameManager.GameOver2();
        }


        if (other.CompareTag("Banana"))
        {
            gameManager.BananaChomp();

            Debug.Log("omnom");
            Destroy(other.gameObject);
            gameManager.UpdateScore(10);
            playerRb.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);

            banz.transform.position = gameObject.transform.position;
            banz.Play();

            mass *= 1.002f;
            playerRb.mass = mass;

            if (drag < 4)
            {
                drag *= 1.017f;
                playerRb.drag = drag;
            }

            boostspeed *= 1.024f;
            boostwait *= 0.88f;


            //gamecondition
        }



        ///original
        //Destroy(gameObject);
        //original
    }


    void smoothLookTowardDirectionOfMovement()
    {
        float moveHorizontal = moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (moveHorizontal != 0 || moveVertical != 0)
        { transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F); }
    }

    void Boundaries()
    {
        if (transform.position.z < -vBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -vBound + 0.1f);
            playerRb.Sleep();
            playerRb.AddForce(Vector3.forward * vspeed * 20);
        }

        if (transform.position.z > vBound)
        {
            playerRb.Sleep();
            transform.position = new Vector3(transform.position.x, transform.position.y, vBound - 0.1f);
            playerRb.AddForce(Vector3.back * vspeed * 20);
        }

        if (transform.position.x < -hBound)
        {
            playerRb.Sleep();
            transform.position = new Vector3(-hBound + 0.1f, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.right * vspeed * 20);
        }

        if (transform.position.x > hBound)
        {
            playerRb.Sleep();
            transform.position = new Vector3(hBound - 0.1f, transform.position.y, transform.position.z);
            playerRb.AddForce(Vector3.left * vspeed * 20);
        }
    } 
    













}
