using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{

    public float wanderDistance = 1;
    public float wanderRadius = 3;
    public float wanderDisplacement = 5;

    public float wanderAngle { get; set; } = 0;

    void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }
        if (gameObjects.Length == 0)
        {
            if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
            {
                movement.ApplyForce(Steering.Wander(this));
            }
        }


        if (gameObjects.Length > 0 && gameObjects[0] != null) 
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * 0);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * 1);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));



    }
}
