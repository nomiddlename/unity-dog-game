using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField]
    Vector3 movementVector = new Vector3(0, 10, 0);

    [SerializeField]
    float period = 1.0f;

    Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (period > 0) {
            float cycles = Time.time / period;
            float movementFactor = Mathf.Abs(Mathf.Sin(cycles * Mathf.PI * 2));
            transform.position = startPosition + (movementFactor * movementVector);
        }
	}
}
