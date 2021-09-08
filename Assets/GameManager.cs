﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl player1;
    private Rigidbody2D player1Rigidbody;

    public PlayerControl player2;
    private Rigidbody2D player2Rigidbody;

    public BallControl ball;
    private Rigidbody2D ballRigidbody;
    private CircleCollider2D ballCollider;

    public int maxScore;
    private bool isDebugWindowsShown = false;
    public Trajectory trajectory;
    

    // Start is called before the first frame update
    void Start()
    {
        player1Rigidbody = player1.GetComponent<Rigidbody2D>();
        player2Rigidbody = player2.GetComponent<Rigidbody2D>();
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }
        if (player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WON");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WON");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        if (isDebugWindowsShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;
            float ballMass = ballRigidbody.mass;
            Vector2 ballVelocity = ballRigidbody.velocity;
            float ballSpeed = ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                "Massa Bola = " + ballMass + "\n" +
                "Velocity Bola = " + ballVelocity + "\n" +
                "Kecepatan Bola = " + ballSpeed + "\n" +
                "Momentum Bola = " + ballMomentum + "\n" +
                "Gesekan Bola = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);
            GUI.backgroundColor = oldColor;
        }
        if (GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowsShown = !isDebugWindowsShown;
            trajectory.enabled = !trajectory.enabled;
        }
        
    }
}
