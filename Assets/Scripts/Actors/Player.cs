﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public int items = 0;
    [HideInInspector]
    public int hp = 1;

    private float accelerationForce = 800;
    private float jumpForce = 4000;

    private Vector2 maxVelocity;

    private bool inputPressed = false;
    private bool jumped = false;
    private bool onAir = false;

    private Rigidbody2D body;

    void Start()
    {        
        hp = Game.config.playerHp;
        maxVelocity = new Vector2(Game.config.playerSpeed, 8.5f);
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Game.isPlaying())
        {
            if (Input.GetKey("space") || checkTouch()) inputPressed = true;
            else inputPressed = false;
        }
    }

    bool checkTouch() {
        var touched = false;
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began || Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
            {
                touched = true;
                print("Touched! :)");
                break;
            }
        }
        return touched;
    }
    
    void FixedUpdate()
    {
        if (Game.isPlaying())
        {
            if (body.velocity.x < maxVelocity.x) body.AddForce(Vector2.right * accelerationForce * Time.deltaTime);

            bool goingUp = false;
            if (inputPressed && body.velocity.y < maxVelocity.y && !jumped) goingUp = true;
            else if (onAir) jumped = true;

            if (goingUp)
            {
                body.AddForce(Vector2.right * accelerationForce * Time.deltaTime);
                body.AddForce(Vector2.up * jumpForce * Time.deltaTime);
                onAir = true;
            }

            //print(body.velocity + " goingUp:" + goingUp + " onAir:" + onAir + " jumped:" + jumped);
        }
        else body.velocity = new Vector2(0, body.velocity.y);
    }

    public void stopJumping()
    {
        onAir = false;
        jumped = false;
    }

    public void itemsUp()
    {
        items++;
    }

    public void hpDown()
    {
        hp--;
        if (hp <= 0) Game.over();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground": stopJumping(); break;
            case "StageEnd": Game.complete(); break;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                hpDown();
                Destroy(other.gameObject);
                break;
            case "Collectable":
                itemsUp();
                Destroy(other.gameObject);
                break;
        }
    }
}
