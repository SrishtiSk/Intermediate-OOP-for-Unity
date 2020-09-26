using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    /// <summary>
    /// Paddle script to move it horizonatlly
    /// 
    /// </summary>
    // Start is called before the first frame update


    Rigidbody2D rigidbody;
    float halfColliderHeight;
    float halfColliderWidth;

    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider2D boxCol =GetComponent<BoxCollider2D>();
        halfColliderWidth = boxCol.size.x / 2;
        halfColliderHeight = boxCol.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            Vector2 position = rigidbody.position;
            position.x += horizontalInput * ConfigurationUtils.PaddleMoveUnitsPerSecond
                * Time.deltaTime;
            position.x = CalculateClampedX(position.x);
            rigidbody.MovePosition(position);
        }
    }
    float CalculateClampedX(float x)
    {
        if(x-halfColliderWidth <ScreenUtils.ScreenLeft )
        {
            x = ScreenUtils.ScreenLeft + halfColliderWidth;
        }
        else if (x + halfColliderWidth > ScreenUtils.ScreenRight)
        {
            x = ScreenUtils.ScreenRight - halfColliderWidth;
        }
        return x;
    }
    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))// && TopCollison(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfColliderWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }
    
}
