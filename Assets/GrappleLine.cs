using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleLine : MonoBehaviour
{
    public PlayerStateMachine player;
    public GrapplingState state;
    public Sprite sprite;

    private void Start()
    {       
        player = GetComponent<PlayerStateMachine>();
        state = GetComponent<GrapplingState>();
        //tate.yourGrappleLineSprite = sprite;
    }

    private void Update()
    {
        if (player.currentStateName == "GrapplingState")
        {
            Debug.Log("Hello World!");
            //state.yourGrappleLineSprite = sprite;
        }
    }
}
