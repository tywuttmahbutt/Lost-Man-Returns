﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Initializing Variables to change the speed of certain actions
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;

    //States (setting up to check if player is alive or not)
    bool isAlive = true;




    //Initializing the component of the object (in this case the player)
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider2D;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        TurningAround();
        Jump();
        //ClimbLadder();
    }


    //This function controls the horizontal movement of the character
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 and +1. (+1 is going right -1 is going left)

        //This is a vector that we create to modify the horizontal movement and letting Unity control the vertical movement
        //Vector 2 means its using 2d space vectors
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);


        //Creating an instance of velocity
        myRigidBody.velocity = playerVelocity;


        //Checking if the player is actually moving
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        
        //If they are running, then activate the running animation
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void TurningAround()
    {

        //When the Absolute Value of the movement in the x direction is greater than zero (Mathf.Epsilon) then the bool will be set to true
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        //If this is true then do this
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }


    }



    private void Jump()
    {

        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;

        }
    }

    /*
     * Doesnt work at the moment
     * 
     * 
     * 
    private void ClimbLadder()
    {
        if(!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
    }

    */




}
