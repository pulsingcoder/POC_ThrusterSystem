using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
    public Rigidbody rb;
    [Header("Thrusters Transform")]
    public Transform negXThruster;
    public Transform posXThruster;
    public Transform negZThruster;
    public Transform posZThruster;
    public Transform mainThruster;

    [Header("VFX")]
    public ParticleSystem negXParticleSystem;
    public ParticleSystem posXParticleSystem;
    public ParticleSystem negZParticleSystem;
    public ParticleSystem posZParticleSystem;
    public ParticleSystem mainThrusterParticleSystem;

    public float moveSpeed = 10f;
    public float torqueMultiplier = 100f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            negZParticleSystem.Play();
        if (Input.GetKey(KeyCode.S))
            posZParticleSystem.Play();
        if (Input.GetKey(KeyCode.A))
            negXParticleSystem.Play();
        if (Input.GetKey(KeyCode.D))
            posXParticleSystem.Play();
        if (Input.GetKey(KeyCode.Space))
            mainThrusterParticleSystem.Play();
    }

    private void FixedUpdate()
    {
        float moveZ = 0f;
        float moveNegX = 0f;
        float movePosX = 0f;

        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        if (Input.GetKey(KeyCode.S))
            moveZ = -1f;
        if (Input.GetKey(KeyCode.A))
            moveNegX = 1f;
        if (Input.GetKey(KeyCode.D))
            movePosX = 1f;


        //float moveX = Input.GetAxis("Horizontal");
        //float moveZ = Input.GetAxis("Vertical");

        rb.AddForce(negZThruster.forward * moveZ * moveSpeed);
        rb.AddForce(posZThruster.forward * moveZ * moveSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(mainThruster.forward * moveSpeed);
        }

        float negXThrust = negXThruster.localPosition.y * moveNegX * moveSpeed;
        float posXThrust = posXThruster.localPosition.y * movePosX * moveSpeed;
        float zTorque = (posXThrust - negXThrust) * torqueMultiplier;

        rb.AddTorque(0f, 0f, zTorque);
    }
}