using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public Transform smoke;

    // Start is called before the first frame update
    void Start()
    {
        smoke.GetComponent< ParticleSystem > ().enableEmission = false;
    }

    // Update is called once per frame
    void OnTriggerEnter2D()
    {
        smoke.GetComponent<ParticleSystem>().enableEmission = true;
    }
}
