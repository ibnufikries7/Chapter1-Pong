using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerControl player;
    [SerializeField]
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D anothercollision)
    {
        if (anothercollision.name == "Ball")
        {
            player.IncrementScore();
            if (player.Score < gameManager.maxScore)
            {
                anothercollision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
