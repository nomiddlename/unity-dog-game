using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("space");
        }
        if (Input.GetKey(KeyCode.A))
        {
            print("rotate left");
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            print("rotate right");  
        }
    }
}
