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

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 40;
        rb = GetComponent<Rigidbody>();
        a = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) //returns true when we are presssing space key
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //it means (0,1,0)
            if (!a.isPlaying)
            {
                a.PlayOneShot(mainEngine);
            }
        }else
        {
            a.Stop();
        }
    }

    void ProcessRotation()
    {
        //we Dont want user to rotate both left and right at the same time, so we use else if
        if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true; // freezing rotation so we can manually rotate
            transform.Rotate(Vector3.forward*rotationThrust*Time.deltaTime); //(0,0,1)
            rb.freezeRotation = false; // unfreezing rotation so physics system can take over

        }else if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true; //  freezing rotation so we can manually rotate
            transform.Rotate(Vector3.back*rotationThrust*Time.deltaTime);//(0,0,-1)
            rb.freezeRotation = false; // unfreezing rotation so physics system can take over
        }
    }
}
