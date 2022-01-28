using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody body;
    float playerHeight;
    bool isGrounded;
    Vector2 rotation;
    const string xAxis = "Mouse X"; //Avoid generating garbag
	const string yAxis = "Mouse Y";
    
    Vector3 velocity;
    Vector3 targetVelocity;

    [SerializeField] LayerMask groundLayermask = default;
    [SerializeField] Transform playerCamera;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float additionalGravity = 10f;
    [SerializeField] float jumpForce = 30f;
    [SerializeField] float sensitivity = 2f;

    void OnEnable()
    {
        playerInput.HoldJump += TryJump;
    }

    void OnDisable()
    {
        playerInput.HoldJump -= TryJump;
    }

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        body = GetComponent<Rigidbody>();

        playerHeight = GetComponent<CapsuleCollider>().height;
    }

    void Update()
    {
        targetVelocity = new Vector3(playerInput.DirectionalInput.x, 0f, playerInput.DirectionalInput.y);
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, 1f);

        targetVelocity *= maxSpeed;

        //change velocity vector from world space to local space
        targetVelocity = transform.TransformDirection(targetVelocity);

        CameraLook();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(groundCheckRay, out hit, playerHeight, groundLayermask))
        {
            isGrounded = hit.distance < (playerHeight/2f + 0.05f);
        }

        body.velocity = new Vector3(targetVelocity.x, body.velocity.y, targetVelocity.z);

    }

    void TryJump()
    {
        if(isGrounded)
        {
            isGrounded = false;
            body.AddForce(transform.up * jumpForce);
        }
    }

    void CameraLook()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -89f, 89f);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat;
        playerCamera.localRotation = yQuat;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + targetVelocity);
        Gizmos.DrawLine(transform.position, transform.position + velocity);
    }
}
