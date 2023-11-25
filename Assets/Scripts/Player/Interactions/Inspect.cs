using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inspect : MonoBehaviour, IInteractable
{

    string hintName = "Look";
    public string description = "A thing.";
    public bool disableOnInteract = false;

    bool disabled = false;

    public string InteractTitle(Controller2D interactor = null)
    {
        if (disabled)
            return "";

        return hintName;
    }

    public void OnInteract(Controller2D interactor = null)
    {
        PlayerMessage.Instance.SetMessage(description);
    }




}
