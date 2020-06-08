using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    //forces acted on player
    [SerializeField] private float upForce;
    [SerializeField] private float sideForce;
    
    //components needed
    private Rigidbody rb;
    public Transform head;
    public Transform cam;

    //for animating camera
    private bool isLeft;
    private bool isRight;

    //which wall is closer
    private float distFromLeft;
    private float distFromRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void wallChecker()
    {
        RaycastHit leftWall;
        RaycastHit rightWall;

        if (Physics.Raycast(head.position, head.right, out rightWall))
        {
            distFromRight = Vector3.Distance(head.position, rightWall.point);
            if(distFromRight < 3f)
            {
                isRight = true;
                isLeft = false;
            }
        }
        if (Physics.Raycast(head.position, -head.right, out leftWall))
        {
            distFromRight = Vector3.Distance(head.position, leftWall.point);
            if (distFromLeft < 3f)
            {
                isRight = false;
                isLeft = true;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wallChecker();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("RunnableWall"))
        {
            rb.useGravity = false;

            Debug.Log("Wall Runnign");

            if (isRight)
            {
                cam.localEulerAngles = new Vector3(0f, 0f, 10f);
            }
            if (isLeft)
            {
                cam.localEulerAngles = new Vector3(0f, 0f, -10f);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("RunnableWall"))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (isRight)
                {
                    rb.AddForce(Vector3.up * upForce * Time.deltaTime);
                    rb.AddForce(-head.right * sideForce * Time.deltaTime);
                }
                if (isLeft)
                {
                    rb.AddForce(Vector3.up * upForce * Time.deltaTime);
                    rb.AddForce(head.right * sideForce * Time.deltaTime);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("RunnableWall"))
        {
            rb.useGravity = true;
            cam.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
}
