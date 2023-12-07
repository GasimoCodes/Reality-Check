using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlySwimmingState : PlayerState
{
    public override string stateName => "Swim";
    Rigidbody2D rb;

    public PlySwimmingState(Controller2D driver, Rigidbody2D rb) : base(driver)
    {
        this.rb = rb;
    }


    public override void Enter()
    {
        // Lets set our goodboy values for regular movement
        driver.playerMaxSpeed = 1;
        driver.jumpMultiplier = 0.5f;
        
    }

    public override void Update()
    {

        //TO-DO: IF NO LONGER IN WATER, RETURN TO PREVIOUS STATE

        //TO-DO: IF ON SURFACE, ALLOW JUMP?

        //TO-DO: Do Swimming/Auto-boyuancy mechanic for water
        //(Unless we handle boyuancy in a more generic way using a component which we just add onto other physics objects too)

        // For now I just pasted the movement code here. Maybe we should make the generic movement as part of driver and do state-specific additions here?
        // Not sure yet


        if (rb.velocity.x == 0f)
        {
            driver.velocity.x = 0f;
        }

        if (driver.moveAction.ReadValue<float>() != 0)
        {
            driver.velocity.x += driver.moveAction.ReadValue<float>() * driver.playerMaxSpeed * Time.deltaTime * 5f;
            driver.velocity.x = math.clamp(driver.velocity.x, -driver.playerMaxSpeed, driver.playerMaxSpeed);
        }

        /* IN AIR */
        if (driver.velocity.x > 0f && driver.isGrounded())
        {
            driver.velocity.x -= Time.deltaTime * 0.25f;
        }

        if (driver.velocity.x < 0f && driver.isGrounded())
        {
            driver.velocity.x += Time.deltaTime * 0.25f;
        }

        /* ON GROUND */
        if (driver.velocity.x > 0f && driver.isGrounded())
        {
            driver.velocity.x -= Time.deltaTime * 8f;
        }

        if (driver.velocity.x < 0f && driver.isGrounded())
        {
            driver.velocity.x += Time.deltaTime * 8f;
        }

        if (driver.velocity.x < driver.playerMaxSpeed / 50f && driver.velocity.x > -driver.playerMaxSpeed / 50f)
        {
            driver.velocity.x = 0;
        }

        rb.velocity = new Vector2(driver.velocity.x, rb.velocity.y);
    }

    public override void Exit()
    {

    }
}
