using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class newMovement : MonoBehaviour
{
    //public CapsueFinder capsueFinder;
    [Range(10f, 100f)]
    public float mouseSensitivity = 100f;

    public LayerMask groundLayers;
    public float jumpForce = 6f;
    public CapsuleCollider col;


    float walkSpeed = 6f;
    float runSpeed = 9f;
    public float moveSpeed;
        
    float vertical;
    float horizontal;
    
    Vector3 deltaPosition;

    Rigidbody rbody;
    
    void Start()
    {
      
        rbody = GetComponent<Rigidbody>();
        rbody.freezeRotation = true;

        moveSpeed = walkSpeed;

        col = GetComponent<CapsuleCollider>();
    }
    void GetInputs()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Sprint"))
        {
            moveSpeed = runSpeed;
            jumpForce = 9f;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            moveSpeed = walkSpeed;
            jumpForce = 6f;
        }
    }
    
    void Update()
    {
        GetInputs();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rbody.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
            
        }
    }
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
        
    }
    private void FixedUpdate()
    {
        
        deltaPosition = ((transform.forward * vertical) + transform.right * horizontal) * moveSpeed * Time.deltaTime;
        rbody.MovePosition(rbody.position + deltaPosition);
    }
}
