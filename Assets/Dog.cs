using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dog : MonoBehaviour {

    [SerializeField] float thrust = 500f;
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] int nextLevel = 0;
    [SerializeField] int thisLevel = 0;
    [SerializeField] AudioClip thrustNoise;
    [SerializeField] AudioClip bumpNoise;
    [SerializeField] AudioClip endLevelNoise;
    [SerializeField] ParticleSystem farts;
    [SerializeField] ParticleSystem bump;
    [SerializeField] ParticleSystem goodBoy;

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
        farts.Stop();
        bump.Play();
        Invoke("BackToStart", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        state = State.Transitioning;
        PlaySound(endLevelNoise);
        farts.Stop();
        goodBoy.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void PlaySound(AudioClip sound)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sound);
    }

    private void LoadNextScene() 
    {
        SceneManager.LoadScene(nextLevel);    
    }

    private void BackToStart()
    {
        SceneManager.LoadScene(thisLevel);
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
            farts.Play();
        }
        else
        {
            audioSource.Stop();
            farts.Stop();
        }
    }
}
