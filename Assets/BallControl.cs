using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    public float xIntialForce = 50;
    public float yInitialForce = 15;
    private Vector2 trajectoryOrigin;

    void ResetBall()
    {
        transform.position = Vector2.zero;

        rigidBody2D.velocity = Vector2.zero;
    }

    
    void PushBall()
    {
        Vector2 velo = GetComponent<Rigidbody2D>().velocity;
        if (velo.x != 0)
        {
            if (velo.x > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(xIntialForce, velo.y);

            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-xIntialForce, velo.y);
            }
        }

        float randomDirection = Random.Range(0, 2);
        if (randomDirection < 1)
        {
            rigidBody2D.AddForce(new Vector2(-xIntialForce, -yInitialForce));
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(xIntialForce, yInitialForce));
        }

        
    }

    

    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        trajectoryOrigin = transform.position;
        rigidBody2D = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
