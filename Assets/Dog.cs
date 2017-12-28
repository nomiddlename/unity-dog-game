using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

    [SerializeField] float thrust = 500f;
    [SerializeField] float rcsThrust = 300f;

    Rigidbody rigidbody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Friendly":
                Debug.Log("OK");
                break;
            default:
                Debug.Log("Dead!");
                break;
        }
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
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
