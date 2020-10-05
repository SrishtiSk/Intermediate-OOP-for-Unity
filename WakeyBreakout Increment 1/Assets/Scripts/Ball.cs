﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //timer start
    Timer moveTimer;
    //timer death
    Timer deathTimer;
    
    // Start is called before the first frame update
    void Start()
    {
       // StartMoving();

        //Start move timer
        moveTimer = gameObject.AddComponent<Timer>();
        moveTimer.Duration = ConfigurationUtils.BallLifeTime;
        moveTimer.Run();

        //start death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallLifeTime;
        deathTimer.Run();
        
    }

    // Update is called once per frame
    void Update()
    {
        //move when time is up
        if (moveTimer.Finished)
        {
            moveTimer.Stop();
            StartMoving();
        }
        //die when time is up
        if (deathTimer.Finished)
        {
            Camera.main.GetComponent<BallSpawner>();
            Destroy(gameObject);
        }
    }
  
    public void StartMoving()
    {
        float angle = -90 * Mathf.Deg2Rad;
        Vector2 force = new Vector2(
            ConfigurationUtils.BallImpulseForce * Mathf.Cos(angle),
            ConfigurationUtils.BallImpulseForce * Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().AddForce(force);
    }
    public void SetDirection(Vector2 direction)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        float speed = rb2d.velocity.magnitude;
        rb2d.velocity = direction * speed;
    }
}
