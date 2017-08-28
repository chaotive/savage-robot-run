﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public bool goingRight = true;
    public float speed = 6f;
         
    private float hAccelerationForce = 800;    
    private Vector2 maxVelocity;
    
    private Rigidbody2D body;
    
    void Start()
    {
        var direction = 1;
        if (!goingRight) direction = -1;
        body = GetComponent<Rigidbody2D>();
        maxVelocity = new Vector2(speed * direction, 8.5f);

        //Player
        //maxVelocity = new Vector2(Game.config.playerSpeed * direction, 8.5f);
        // Moving Enemy
        //maxVelocity = new Vector2(Game.config.movingEnemiesSpeed * -1, 8.5f);
    }
    
    void FixedUpdate()
    {
        if (GameController.isPlaying())
        {
            
            if (goingRight && body.velocity.x < maxVelocity.x) body.AddForce(Vector2.right * hAccelerationForce * Time.deltaTime);
            else if (body.velocity.x > maxVelocity.x) body.AddForce(Vector2.left * hAccelerationForce * Time.deltaTime);
            
            //print(name + " " + body.velocity);
        }
        else body.velocity = new Vector2(0, body.velocity.y); //TODO: Synchronize with jump velocity somehow

        /*
        // Moving Enemy
        if (Game.isPlaying())
        {
            if (body.velocity.x > maxVelocity.x) body.AddForce(Vector2.left * accelerationForce * Time.deltaTime);
            //print("Moving enemy: " + body.velocity);
        }
        else body.velocity = new Vector2(0, body.velocity.y);
        */
    }

}