using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 1f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space)){
          //  rb.AddRelativeForce(0, 1 ,0);
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }  
    }

    void ProcessRotation() {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateThrust);
        }

        else if(Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotateThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manullay rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotationso the physics system can take over
    }
}
