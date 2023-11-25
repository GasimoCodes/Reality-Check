using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player State Machine
/// </summary>
public class Controller2D : MonoBehaviour
{
    public Vector2 velocity;
    public GameObject virtualCameraShift;


    // Inputs (Input class is just an generated wrapper for my Input file under Data/Input
    [Header(header: "Inputs")]
    public InputActionAsset inputMap;
    // This is cached for ez fast read
    InputAction moveAction;

    // Physics
    Rigidbody2D rb;
    public LayerMask environmentMask;

    // Settings
    public float playerMaxSpeed = 2;
    public float jumpMultiplier = 1;
    public float playerHeight = 0.8f;


    private void Awake()
    {
        moveAction = inputMap.FindAction("Move");
        moveAction.performed += OnMove;
        inputMap.FindAction("Interact").performed += OnInteract;
        inputMap.FindAction("Jump").performed += OnJump;
        inputMap.FindAction("Rift").performed += OnRift;

        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //velocity = new Vector2(, 0);
        // We make this better later
        rb.velocity = new Vector2(moveAction.ReadValue<float>() * playerMaxSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("MOVE: " + context.ReadValue<float>());
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded())
            rb.AddForce(new Vector2(0, 300 * jumpMultiplier));
    }

    public void OnRift(InputAction.CallbackContext context)
    {
        
    }


    /// <summary>
    /// Casts a tiny ray below player to check if they do be standing at smth
    /// TODO: Ignore some layers?
    /// </summary>
    /// <returns></returns>
    public bool isGrounded()
    {

            float raycastLength = playerHeight/2f;

            // Cast a ray from the player's position downward
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, environmentMask);

            // Check if the ray hits something on the ground layer
            //if(hit.collider != null)
            //Debug.Log("Hit: " + hit.collider.name);
            
            return hit.collider != null;
    }




    /// <summary>
    /// This makes sure Unity wont attempt to call OnJump etc. even after the player ceased existing
    /// </summary>
    void OnDestroy()
    {
        UnMapMaps();
    }

    private void UnMapMaps()
    {
        inputMap.FindAction("Move").performed -= OnMove;
        inputMap.FindAction("Interact").performed -= OnInteract;
        inputMap.FindAction("Jump").performed -= OnJump;
        inputMap.FindAction("Rift").performed -= OnRift;
    }

}
