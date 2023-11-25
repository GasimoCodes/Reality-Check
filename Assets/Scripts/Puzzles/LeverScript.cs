using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour, IInteractable
{
    private bool isFlipped = false;

	public string interactHintTrue = "True";
	public string interactHintFalse = "False";

	public UnityEvent On;
	public UnityEvent Off;

	public string InteractTitle(Controller2D interactor = null)
	{
		return isFlipped ? interactHintTrue : interactHintFalse;
	}

	public void OnInteract(Controller2D interactor = null)
	{
		isFlipped = !isFlipped;

		if (isFlipped)
			On.Invoke();
		else
			Off.Invoke();
	}
}
