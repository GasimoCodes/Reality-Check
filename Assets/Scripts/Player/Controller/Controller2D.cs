using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player State Machine
/// </summary>
public class Controller2D : MonoBehaviour
{
    [Tooltip("This is the object which contains cam and sprite, we flip it based on ply facing dir")]
    public GameObject plyObjectFlipper;

    public Interactor2D interactor;
    public SpriteRenderer sprite;
    public Animator animator;

    // Inputs (Input class is just an generated wrapper for my Input file under Data/Input
    [Header(header: "Inputs")]
    public InputActionAsset inputMap;
    // This is cached for ez fast read
    public InputAction moveAction;

    [Header(header: "Physics")]
    Rigidbody2D rb;
    public LayerMask environmentMask;

    [Header(header: "Sounds")]
    public AudioClip jumpSound;

    [Header(header: "State Management")]
    private PlayerState currentState;
    private PlayerState lastState;

    [Header(header: "Settings")]
    public float playerMaxSpeed = 2;
    public float jumpMultiplier = 1;
    public float playerHeight = 0.8f;
    public bool lockMovement = false;

    [Header(header: "Runtimes (ReadOnly)")]
    public Vector2 velocity;
    public string currentStateName;

    private void Awake()
    {
        moveAction = inputMap.FindAction("Move");
        moveAction.performed += OnMove;
        inputMap.FindAction("Interact").performed += OnInteract;
        inputMap.FindAction("Jump").performed += OnJump;
        inputMap.FindAction("Rift").performed += OnRift;

        rb = this.GetComponent<Rigidbody2D>();

        // Set first state :3
        currentState = new PlyWalkState(this, this.rb);
    }

    /// <summary>
    /// This sets a next state
    /// </summary>
    /// <param name="nextState"></param>
    public void SetState(PlayerState nextState)
    {
        currentState.Exit();
        lastState = currentState;
        currentState = nextState;
        currentState.Enter();
        currentStateName = currentState.stateName;
    }

    /// <summary>
    /// This returns from the current state to any previous state
    /// </summary>
    public void PreviousState()
    {
        if (lastState != null)
            SetState(lastState);
    }


    private void Update()
    {
        // Do update loop
        currentState.Update();


        // Update Animator with speed values
        animator.SetFloat("SpeedX", math.abs(rb.velocity.x));
        animator.SetFloat("SpeedY", math.abs(rb.velocity.y));

    }

    public void OnMove(InputAction.CallbackContext context)
    {

        if (lockMovement)
            return;

        if (context.ReadValue<float>() < 0)
        {
            plyObjectFlipper.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
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
        if (lockMovement)
            return;


        if (isGrounded())
        {
            rb.velocityY = 0.0f;
            rb.AddForce(new Vector2(0, 300 * jumpMultiplier));
            this.GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
    }

    public void OnRift(InputAction.CallbackContext context)
    {
        if (lockMovement)
            return;


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
        float raycastLength = playerHeight / 2f;

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
