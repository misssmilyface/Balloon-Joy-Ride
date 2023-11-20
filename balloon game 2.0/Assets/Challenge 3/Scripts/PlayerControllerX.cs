﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public bool isOffGround = false;
    private Vector3 startPos;
    public float floatForce = 10;
    public float gravityModifier = 2f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public float upBound = 20;
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    private float upperBound = 18f;
    private float yGameOver = -0.15f;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        startPos = transform.position;
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
        else if (gameOver == true)
        {
            transform.position = new Vector3(transform.position.x,yGameOver,transform.position.z);
        }
        /*if (transform.position.y > upBound)
            {
                transform.position = new Vector3(transform.position.x, upBound, transform.position.z);}
        startPos = transform.position;*/
        Boundary();
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }
    
    private void Boundary()
    {
        if (transform.position.y > upperBound)
        {
            transform.position = new Vector3(transform.position.x,upperBound,transform.position.z);
        }
    }

    
}
