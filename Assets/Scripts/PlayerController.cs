using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// TODO: import UnityEngine.InputSystem and UnityEngine.SceneManagement (done)


public class PlayerController : MonoBehaviour
{
    // TODO: add component references (done)
    Rigidbody rb;
    Transform tr;
    SphereCollider col;

    // TODO: add variables for speed, jumpHeight, and respawnHeight (done)
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float respawnheight = -10f;

    // TODO: add variable to check if we're on the ground (done)
    bool onGround = false;

    UnityEvent flatten;
    UnityEvent unflatten;   


    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get references to the components attached to the current GameObject (done)
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        col = GetComponent<SphereCollider>();

        if (flatten == null)
        {
            flatten = new UnityEvent();
        }

        flatten.AddListener(Flatten);

        if (unflatten == null)
        {
            unflatten = new UnityEvent();
        }

        unflatten.AddListener(Unflatten);
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: check if player is under respawnHeight and call Respawn() (done)
        if (rb.position.y < respawnheight)
        {
            Respawn();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && flatten != null)
        {
            flatten.Invoke();
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift) && unflatten != null)
        {
            unflatten.Invoke();
        }
        
    }

    void OnJump()
    {
        // TODO: check if player is on the ground, and call Jump() (done)
        if (onGround) 
        {
            Jump();
        }

    }

    private void Jump()
    {
        // TODO: Set the y velocity to some positive value while keeping the x and z whatever they were originally (done)
        rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
    }

    void OnMove(InputValue moveVal)
    {
        //TODO: store input as a 2D vector and call Move() (done)
        Vector2 moveData = moveVal.Get<Vector2>();

        Move(moveData.x, moveData.y);

    }

    private void Move(float x, float z)
    {
        // TODO: Set the x & z velocity of the Rigidbody to correspond with our inputs while keeping the y velocity what it originally is. (done)
        rb.velocity = new Vector3(x * speed, rb.velocity.y, z * speed);

    }

    void OnCollisionEnter(Collision collision)
    {
        // This function is commonly useful, but for our current implementation we don't need it

    }

    void OnCollisionStay(Collision collision)
    {
        // TODO: Check if we are in contact with the ground. If we are, note that we are grounded (done)
        onGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        // TODO: When we leave the ground, we are no longer grounded (done)
        onGround = false;
    }

    private void Respawn()
    {
        // TODO: reload current scene
        rb.position = new Vector3(3, 3, 3);
    }

    private void Flatten()
    {
        tr.localScale = new Vector3(tr.localScale.x * 2f, tr.localScale.y * 0.5f, tr.localScale.z * 2f);
        col.radius *= 0.25f;
    }

    private void Unflatten()
    {
        tr.localScale = new Vector3(tr.localScale.x * 0.5f, tr.localScale.y * 2f, tr.localScale.z * 0.5f);
        col.radius *= 4f;
    }
}
