using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericInteractable : MonoBehaviour, IInteractable
{

    string hintName = "Use";
    public UnityEvent OnInteractEvents;
    public bool disableOnInteract = false;

    bool disabled = false;

    public string InteractTitle(Controller2D interactor = null)
    {
        if (disabled)
            return "";

        return "Use";
    }

    public void OnInteract(Controller2D interactor = null)
    {
        OnInteractEvents.Invoke();
    }




}
