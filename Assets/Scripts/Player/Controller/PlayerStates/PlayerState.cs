using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerState
{

    protected Controller2D driver;

    public PlayerState(Controller2D driver)
    {
        this.driver = driver;
    }

    /// <summary>
    /// Name of the state
    /// </summary>
    public abstract string stateName { get; } // For debug

    /// <summary>
    /// Called when the player enters this state
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// Called every Update() when this state is active. Please dont forget to write exit conditions (!) by assigning nextState in the 
    /// Driver
    /// </summary>
    public abstract void Update();

    /// <summary>
    /// Called upon exiting. 
    /// </summary>
    public abstract void Exit();

}
