using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    Rigidbody body;
    float playerHeight;
    bool isGrounded;
    bool isNearWall;
    Vector2 rotation;
    const string xAxis = "Mouse X"; //Avoid generating garbag
	const string yAxis = "Mouse Y";
    
    Vector3 velocity;
    Vector3 targetVelocity;
    Vector3 awayFromClosestWall;

    [SerializeField] LayerMask groundLayermask = default;
    [SerializeField] LayerMask wallLayermask = default;
    [SerializeField] Transform playerCamera;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float maxAcceleration = 80f;
    [SerializeField] float midAirAccelerationModifier = 0.6f;
    [SerializeField] float additionalGravity = 10f;
    [SerializeField] float jumpForce = 30f;
    [SerializeField] float sensitivity = 2f;
    [SerializeField] float wallJumpCooldown;
    [SerializeField] float interactionRayDistance = 2f;

    bool tryJumpNextPhysicsFrame = false;
    bool canWallJump = true;
    float wallJumpWaitTimer = 0f;
    
    Ray interactionRay;
    RaycastHit interactionRayHit;


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

        if(!canWallJump)
        {
            wallJumpWaitTimer += Time.deltaTime;
            if(wallJumpWaitTimer > wallJumpCooldown)
            {
                canWallJump = true;
                wallJumpWaitTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(groundCheckRay, out hit, playerHeight, groundLayermask))
        {
            isGrounded = hit.distance < (playerHeight/2f + 0.01f);
        }

        velocity = body.velocity;
        velocity.y = 0f;

        Vector3 velocityDir = targetVelocity.normalized;

        float maxSpeedChange = (maxAcceleration * Time.deltaTime);
        if(!isGrounded)
        {
            maxSpeedChange *= midAirAccelerationModifier;
        }

        velocity = Vector3.MoveTowards(velocity, targetVelocity, maxSpeedChange);

        body.velocity = new Vector3(velocity.x, body.velocity.y, velocity.z);
        //body.velocity = new Vector3(targetVelocity.x, body.velocity.y, targetVelocity.z);

        if(tryJumpNextPhysicsFrame)
        {
            if(isGrounded)
            {
                body.AddForce(Vector3.up * jumpForce);
                tryJumpNextPhysicsFrame = false;
                isGrounded = false;
            }
            else if(isNearWall && canWallJump)
            {
                body.AddForce(Vector3.up * jumpForce);
                body.AddForce(awayFromClosestWall.normalized * jumpForce);
                tryJumpNextPhysicsFrame = false;
                canWallJump = false;
                Vector3 vel = body.velocity;
                vel.y = 0;
                body.velocity = vel;
            }
            tryJumpNextPhysicsFrame = false;
        }

    }

    void LateUpdate()
    {
        CameraLook();

        interactionRay = new Ray(playerCamera.position, playerCamera.forward);

        if(Physics.Raycast(interactionRay, out interactionRayHit, interactionRayDistance))
        {
            if (interactionRayHit.collider.gameObject.GetComponent<IInteractable>() != null)
			{
				var interactable = interactionRayHit.collider.gameObject.GetComponent<IInteractable>();
				interactable.Hover();
			}
        }
        if (Input.GetKeyDown(KeyCode.E))
		{
			if(Physics.Raycast(interactionRay, out interactionRayHit, interactionRayDistance ))
			{
				if (interactionRayHit.collider.gameObject.GetComponent<IInteractable>() != null)
				{
					var interactable = interactionRayHit.collider.gameObject.GetComponent<IInteractable>();
					interactable.Interact(transform);
				} //Check other interactables here
			}
		}
    }

    void TryJump()
    {
        tryJumpNextPhysicsFrame = true;
    }



    void CameraLook()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -89f, 89f);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
		var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        //transform.localRotation = xQuat;
        body.MoveRotation(xQuat);
        playerCamera.localRotation = yQuat;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + targetVelocity);
        Gizmos.DrawLine(transform.position, transform.position + velocity);
    }

    void OnTriggerStay(Collider other)
    {
        if(wallLayermask == (wallLayermask | (1 << other.gameObject.layer)))
        {
            isNearWall = true;
            awayFromClosestWall = transform.position - other.ClosestPointOnBounds(transform.position);
            awayFromClosestWall.y = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isNearWall = false;
    }
}
