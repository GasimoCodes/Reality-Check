using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player State Machine
/// </summary>
public class Controller2D : MonoBehaviour
{
    public Vector2 velocity;
    [Tooltip("This is the object which contains cam and sprite, we flip it based on ply facing dir")]
    public GameObject plyObjectFlipper;
    public Interactor2D interactor;
    public SpriteRenderer sprite;
    public Animator animator;

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

        animator.SetFloat("SpeedX", math.abs(rb.velocity.x));
        animator.SetFloat("SpeedY", math.abs(rb.velocity.y));
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0)
        {
            plyObjectFlipper.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            plyObjectFlipper.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactor.DoInteract(this);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            rb.velocityY = 0.0f;
            rb.AddForce(new Vector2(0, 300 * jumpMultiplier));
        }
    }

    public void OnRift(InputAction.CallbackContext context)
    {
        ScreenFader.Instance.DoFlash(new Color(1, 1, 1, 0.5f), 0.5f);
        ScreenFX.Instance.ShakeCurrentCamera();
        RiftManager.Instance.ToggleRift();
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
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.001f, 0.001f), 0, Vector2.down, raycastLength, environmentMask);

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
