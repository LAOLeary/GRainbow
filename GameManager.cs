using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AudioSource boombox;
    private GameObject player;
    public AudioClip greenBoom;
    public AudioClip yellowBoom;
    public AudioClip bananaChomp;
    public AudioClip boost;
    public AudioClip pewpew;

    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOver1Text;
    public TextMeshProUGUI gameOver2Text;

    // Start is called before the first frame update
    void Start()
    {
        boombox = GetComponent<AudioSource>();
        score = 0;
        scoreText.text = "Score: " + score;
        UpdateScore(0);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GreenBoom()
    {
        boombox.PlayOneShot(greenBoom, 1.0f);
    }

    public void YellowBoom()
    {
        boombox.PlayOneShot(yellowBoom, 1.0f);
    }

    public void BananaChomp()
    {
        boombox.PlayOneShot(bananaChomp, 1.0f);
    }

    public void Boost()
    {
        boombox.PlayOneShot(boost, 2.0f);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver1()
    {
        gameOver1Text.gameObject.SetActive(true);
    }

    public void GameOver2()
    {
        gameOver2Text.gameObject.SetActive(true);
    }

    public void Pewpew()
    {
        boombox.PlayOneShot(pewpew, 0.6f);
    }

}
