using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactor2D : MonoBehaviour
{
    public GameObject gameplayHint;
    public LayerMask interactionMask;
    
    public IInteractable interactable;
    public GameObject interactablePos;
    public Camera cam;

    TextMeshProUGUI hintText;


    private void Awake()
    {
        hintText = gameplayHint.GetComponentInChildren<TextMeshProUGUI>();
        cam = Camera.main;
    }

    /// <summary>
    /// Call this to interact with obj
    /// </summary>
    /// <param name="player"></param>
    public void DoInteract(Controller2D player)
    {
        if (interactable != null && interactable.InteractTitle(player) != "")
        {
            interactable.OnInteract(player);
        }
    }

    private void FixedUpdate()
    {
        if (interactable != null)
        {
            // Replace this with the position of the object + small offset Vector3.Up to have it not cover small items
            gameplayHint.SetActive(true);
            hintText.text = interactable.InteractTitle();

            Vector3 screenPosition = cam.WorldToScreenPoint(interactablePos.transform.position + Vector3.up * 0.1f);
            
            // Set the screenspace position to the gameplayHint
            gameplayHint.transform.position = screenPosition;

        }
        else
        {
            gameplayHint.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("ENTER " + collision.gameObject.name);
        interactablePos = collision.gameObject;
        interactable = collision.gameObject.GetComponent<IInteractable>();
        
    }

    private void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("LEAVE " + collision.gameObject.name);

        // If we left a different interactable lets ignore
        if (collision.gameObject.GetComponent<IInteractable>() == interactable)
            interactable = null;
    }



}
