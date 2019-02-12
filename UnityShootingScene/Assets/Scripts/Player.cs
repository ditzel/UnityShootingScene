using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public InputStr Input;
    public struct InputStr
    {
        public float LookX;
        public float LookZ;
        public float RunX;
        public float RunZ;
        public bool Jump;

        public bool SwitchToAK;
        public bool SwitchToPistol;
        public bool Shoot;
    }

    public float Speed = 10f;
    public const float JumpForce = 7f;

    protected Rigidbody Rigidbody;
    protected Quaternion LookRotation;
    
    public GameObject AKBack;
    public GameObject AKHand;
    public GameObject PistolBack;
    public GameObject PistolHand;

    protected Animator Animator;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        AKBack.SetActive(true);
        AKHand.SetActive(false);
        PistolBack.SetActive(true);
        PistolHand.SetActive(false);
    }

    void FixedUpdate()
    {

        var inputRun = Vector3.ClampMagnitude(new Vector3(Input.RunX, 0, Input.RunZ), 1);
        var inputLook = Vector3.ClampMagnitude(new Vector3(Input.LookX, 0, Input.LookZ), 1);

        Rigidbody.velocity = new Vector3(inputRun.x * Speed, Rigidbody.velocity.y, inputRun.z * Speed);

        //rotation to go target
        if (inputLook.magnitude > 0.01f)
            LookRotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, inputLook, Vector3.up), Vector3.up);

        transform.rotation = LookRotation;

        var forwardP = Vector3.Project(Rigidbody.velocity, transform.forward);
        Animator.SetFloat("SpeedForward", forwardP.magnitude * Mathf.Sign(forwardP.z));
        var rightP = Vector3.Project(Rigidbody.velocity, transform.right);
        Animator.SetFloat("SpeedSideward", rightP.magnitude * Mathf.Sign(rightP.x));

        if (Input.Shoot)
        {
            Animator.SetTrigger("Shoot");
        }

        if (Input.SwitchToAK)
        {
            Input.SwitchToAK = false;
            Animator.SetBool("AK", true);
            Animator.SetBool("Pistol", false);
        }

        if (Input.SwitchToPistol)
        {
            Input.SwitchToPistol = false;
            Animator.SetBool("AK", false);
            Animator.SetBool("Pistol", true);
        }
    }
}