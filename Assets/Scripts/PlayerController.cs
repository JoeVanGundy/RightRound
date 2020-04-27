using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ObjectToRotateAround;
    public int rotateSpeed;
    public float jumpVelocity = 5;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public LayerMask groundLayers;
    public Rigidbody rb;

    private bool playerJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // private bool IsGrounded()
    // {
    //     CapsuleCollider col = GetComponent<CapsuleCollider>();
    //     return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers);
    // }

    void FixedUpdate() {
        if(playerJump == true) {
            playerJump = false;
            rb.velocity = Vector3.up * jumpVelocity;
        }
        if(rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Update()
    {
        transform.RotateAround(new Vector3(0,0,0), Vector3.down, rotateSpeed * Time.deltaTime);
        if(Input.GetButtonDown("Jump")) {
            playerJump = true;
        }
    }


    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     // transform.RotateAround(new Vector3(0,0,0), Vector3.down, rotateSpeed * Time.deltaTime);

    //     // if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
    //     //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //     // }
    // }
}
