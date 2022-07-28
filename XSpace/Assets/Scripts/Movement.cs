using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine = null;
    [SerializeField] ParticleSystem mainThrusterParticles = null;
    [SerializeField] ParticleSystem leftThrusterParticles = null;
    [SerializeField] ParticleSystem rightThrusterParticles = null;

    Rigidbody rb = null;
    AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                
            }
            if (!mainThrusterParticles.isPlaying)
            {
                mainThrusterParticles.Play();
            }
                        
        }
        else
        {
            audioSource.Stop();
            mainThrusterParticles.Stop();
        }
    }

    void ProcessRotation()
    {
        
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) )
        {
           
            ApplyRotation(-rotationThrust);
            if (!leftThrusterParticles.isPlaying)
            {
                leftThrusterParticles.Play();
            }
            

        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) )
        {
            ApplyRotation(rotationThrust);
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        else
        {
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    void ApplyRotation(float rt)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rt);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
    }
}
