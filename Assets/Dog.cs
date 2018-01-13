﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dog : MonoBehaviour {

    [SerializeField] float thrust = 500f;
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] AudioClip thrustNoise;
    [SerializeField] AudioClip bumpNoise;
    [SerializeField] AudioClip endLevelNoise;

    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State { Alive, Dead, Transitioning };
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive) 
        {
            Thrust();
            Rotate();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }

        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dead;
        PlaySound(bumpNoise);
        Invoke("BackToStart", 1f);
    }

    private void StartSuccessSequence()
    {
        state = State.Transitioning;
        PlaySound(endLevelNoise);
        Invoke("LoadNextScene", 1f);
    }

    private void PlaySound(AudioClip sound)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sound);
    }

    private void LoadNextScene() 
    {
        SceneManager.LoadScene(1);    
    }

    private void BackToStart()
    {
        SceneManager.LoadScene(0);
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustNoise);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
