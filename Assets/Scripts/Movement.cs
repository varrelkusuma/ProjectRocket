using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float strength = 10f;
    [SerializeField] float rotation = 10f;

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

        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * strength * Time.deltaTime);
        }

    }

    void ProcessRotation() {

        if (Input.GetKey(KeyCode.A)) {
            rb.freezeRotation = true; //freezing rotation so we can manually rotate
            transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
            rb.freezeRotation = false; //unfreezing system so physics system can takeover
        }

        else if (Input.GetKey(KeyCode.D)) {
            rb.freezeRotation = true;  //freezing rotation so we can manually rotate
            transform.Rotate(-Vector3.forward * rotation * Time.deltaTime);
            rb.freezeRotation = false; //unfreezing system so physics system can takeover
        }
    }
}
