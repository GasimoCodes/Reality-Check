using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlyWalkState : PlayerState
{
    public override string stateName => "Walk";
    Rigidbody2D rb;

    public PlyWalkState(Controller2D driver, Rigidbody2D rb) : base(driver)
    {
        this.rb = rb;
    }


    public override void Enter()
    {
        // Lets set our goodboy values for regular movement
        driver.playerMaxSpeed = 2;
        driver.jumpMultiplier = 1;
        driver.playerHeight = 0.8f;
    }

    public override void Update()
    {

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
