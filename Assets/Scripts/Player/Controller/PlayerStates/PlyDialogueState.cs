using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlyDialogueState : PlayerState
{
    public override string stateName => "Dialogue";
    
    public PlyDialogueState(Controller2D driver) : base(driver)
    {
    }


    public override void Enter()
    {
        driver.lockMovement = true;
    }

    
    public override void Update()
    {
        // Just give control in over to the dialogueManager or smth, not our concern!
    }

    public override void Exit()
    {
        driver.lockMovement = false;
    }
}
