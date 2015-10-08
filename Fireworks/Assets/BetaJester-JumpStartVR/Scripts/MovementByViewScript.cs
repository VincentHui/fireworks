using UnityEngine;
using System.Collections;
using System;

public class MovementByViewScript : MonoBehaviour
{

    public Transform LookControlObj;
    public float Speed;
    float initialSpeed;

    public Camera cam;

    private Vector3 m_GroundContactNormal;
    Rigidbody m_RigidBody;


    internal Vector3 AdditionalVelocity = Vector3.zero;

    public float SpeedDecayRate = 0.1f;

    // Use this for initialization
    void Start()
    {
        m_RigidBody = this.GetComponent<Rigidbody>();
        initialSpeed = Speed;
    }
    
    internal Vector3 desiredMove = Vector3.zero;
    internal bool AddSpeedBoost = false;

    public float MomentumIncrease = 100f;

    void FixedUpdate()
    {
        ////Set moveDirection to the vertical axis (up and down keys) * speed 
        ////For smoother movement use Input.GetAxis instead of Input.GetAxisRaw 
        //Vector3 moveDirection = cam.transform.forward * Speed * Time.deltaTime;
        ////Transform the vector3 to local space 
        //moveDirection = transform.TransformDirection(moveDirection);
        ////set the velocity, so you can move 
        //m_RigidBody.velocity = moveDirection;

        m_RigidBody.angularVelocity = Vector3.zero;

        desiredMove = cam.transform.forward * 1 + cam.transform.right * 0;


        desiredMove.x = desiredMove.x * Speed * Time.deltaTime;
        desiredMove.z = desiredMove.z * Speed * Time.deltaTime;
        desiredMove.y = 0;// m_RigidBody.velocity.y;

        //m_RigidBody.velocity = desiredMove;//, ForceMode.Acceleration);
        //m_RigidBody.velocity += (desiredMove);

        m_RigidBody.MovePosition(this.transform.position + desiredMove);

        //AdditionalVelocity = Vector3.Lerp(AdditionalVelocity, Vector3.zero, Time.deltaTime);

        Speed = Mathf.Lerp(Speed, initialSpeed, Time.deltaTime * SpeedDecayRate);

    }

    internal void AddMomentum()
    {
        Speed += MomentumIncrease;
    }
}
