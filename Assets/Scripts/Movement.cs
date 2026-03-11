using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float mainThrust = 1000f;
    float rotationThrust = 100f;
    [SerializeField]AudioClip mainEngine;
    Rigidbody rb;
    AudioSource a;

    //Particle System for Rocket 
    [SerializeField]ParticleSystem mainEngineParticles;
    [SerializeField]ParticleSystem leftThrusterParticles;
    [SerializeField]ParticleSystem rightThrusterParticles;

    bool isThrusting;
    bool isRotatingLeft;
    bool isRotatingRight;

    // Start is called before the first frame update
    void Start()
    {
       // Application.targetFrameRate = 40;
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate; // to make smooth transitions
        a = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isThrusting = Input.GetKey(KeyCode.Space);
        isRotatingLeft = Input.GetKey(KeyCode.A);
        isRotatingRight = Input.GetKey(KeyCode.D);
        ProcessThrustEffect();
        ProcessRotationEffect();
    }

    //Fixed update runs in sync with phyics so we apply forces here 
     private void FixedUpdate() {
        if(isThrusting)
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.fixedDeltaTime); //it means (0,1,0)
        }
        if(isRotatingLeft)
        {
            rb.freezeRotation = true; // freezing rotation so we can manually rotate
            transform.Rotate(Vector3.forward*rotationThrust*Time.fixedDeltaTime); //(0,0,1)
            rb.freezeRotation = false; // unfreezing rotation so physics system can take over
        }else if(isRotatingRight)
        {
            rb.freezeRotation = true; //  freezing rotation so we can manually rotate
            transform.Rotate(Vector3.back*rotationThrust*Time.fixedDeltaTime);//(0,0,-1)
            rb.freezeRotation = false; // unfreezing rotation so physics system can take over
        }
    }

    // void ProcessThrust()
    // {
    //     if (Input.GetKey(KeyCode.Space)) //returns true when we are presssing space key
    //     {
    //         rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //it means (0,1,0)
    //         if (!a.isPlaying)
    //         {
    //             a.PlayOneShot(mainEngine);
    //         }
    //         //Display particles when we are thrusting 
    //         if (!mainEngineParticles.isPlaying)
    //         {
    //             mainEngineParticles.Play();
    //         }
    //     }else
    //     {
    //         a.Stop();
    //         //stop the main engine Particles 
    //         mainEngineParticles.Stop();
    //     }
    // }

    void ProcessThrustEffect()
    {
        if(isThrusting)
        {
            if (!a.isPlaying)
            {
                a.PlayOneShot(mainEngine);
            }
            //Display particles when we are thrusting 
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }else
        {
            //Stop the audio when we are not thrusting and particles
            a.Stop();
            mainEngineParticles.Stop();
        }
    }

    void ProcessRotationEffect()
    {
        if(isRotatingLeft)
        {
             if (!leftThrusterParticles.isPlaying)
            {
            leftThrusterParticles.Play();    
            }
        }else if(isRotatingRight)
        {
             if (!rightThrusterParticles.isPlaying)
            {
            rightThrusterParticles.Play();    
            }
        }else
        {
            //if nothing then stop the particles 
            leftThrusterParticles.Stop();
            rightThrusterParticles.Stop();
        }
    }

    // void ProcessRotation()
    // {
    //     //we Dont want user to rotate both left and right at the same time, so we use else if
    //     if (Input.GetKey(KeyCode.A))
    //     {
    //         rb.freezeRotation = true; // freezing rotation so we can manually rotate
    //         transform.Rotate(Vector3.forward*rotationThrust*Time.deltaTime); //(0,0,1)
    //         rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    //         if (!leftThrusterParticles.isPlaying)
    //         {
    //         leftThrusterParticles.Play();    
    //         }

    //     }else if (Input.GetKey(KeyCode.D))
    //     {
    //         rb.freezeRotation = true; //  freezing rotation so we can manually rotate
    //         transform.Rotate(Vector3.back*rotationThrust*Time.deltaTime);//(0,0,-1)
    //         rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    //         if (!rightThrusterParticles.isPlaying)
    //         {
    //         rightThrusterParticles.Play();    
    //         }
    //     }
    //     else
    //     {
    //         leftThrusterParticles.Stop();
    //         rightThrusterParticles.Stop();
    //     }
    // }
}
