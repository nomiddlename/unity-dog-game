using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    Vector3 rotationPoint = new Vector3(0, 0, 0);
    [SerializeField]
    float speed = 1.0f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotationThisFrame = speed * Time.deltaTime;
        transform.RotateAround(rotationPoint, Vector3.forward, rotationThisFrame);
    }
}
