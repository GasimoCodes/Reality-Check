using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Returns the interaction hint name. Return "" to not show any hint
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public string InteractTitle(Controller2D interactor = null);

    /// <summary>
    /// This is called when we want to interact with this obj
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public void OnInteract(Controller2D interactor = null);
}
