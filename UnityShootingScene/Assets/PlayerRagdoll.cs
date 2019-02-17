using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{

    protected Animator Animator;
    protected Rigidbody Rigidbody;
    protected Collider Collider;
    protected Player Player;

    protected Collider[] ChildrenCollider;
    protected Rigidbody[] ChildrenRigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Player = GetComponent<Player>();

        ChildrenCollider = GetComponentsInChildren<Collider>();
        ChildrenRigidbody = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    public void RadollSetActive(bool active)
    {
        if (Animator == null)
            Awake();

        //children
        foreach(var collider in ChildrenCollider)
            collider.enabled = active;
        foreach(var rigidbody in ChildrenRigidbody)
        {
            rigidbody.isKinematic = false; //just to prevent warnings will be overwritten afterwards
            rigidbody.collisionDetectionMode = (active) ? CollisionDetectionMode.Continuous : CollisionDetectionMode.ContinuousSpeculative;
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        //parent
        Rigidbody.isKinematic = false; //just to prevent warnings will be overwritten afterwards
        Rigidbody.collisionDetectionMode = (active) ? CollisionDetectionMode.ContinuousSpeculative : CollisionDetectionMode.Continuous;
        Collider.enabled = !active;
        Rigidbody.isKinematic = active;
        Rigidbody.detectCollisions = !active;
        Animator.enabled = !active;
        Player.enabled = !active;

    }
}
