using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    public Perception flockPerception;
    public AutonomousAgentData data;
    public ObstacleAvoidance obstacleAvoidance;

    public float wanderAngle { get; set; } = 0;

    void Update()
    {
        
        var gameObjects = perception.GetGameObjects();
        foreach (var gameObject in gameObjects)
        {
            Debug.DrawLine(transform.position, gameObject.transform.position);
        }

        if (obstacleAvoidance.IsObstacleInFront())
        {
            Vector3 direction = obstacleAvoidance.GetOpenDirection();
            movement.ApplyForce(Steering.CalculateSteering(this, direction) * data.obstacleWeight);
        }


        if (gameObjects.Length > 0 && gameObjects[0] != null) 
        {
            movement.ApplyForce(Steering.Seek(this, gameObjects[0]) * data.seekWeight);
            movement.ApplyForce(Steering.Flee(this, gameObjects[0]) * data.fleeWeight);
        }

        Vector3 position = transform.position;
        position = Utilities.Wrap(position, new Vector3(-20, -20, -20), new Vector3(20, 20, 20));
        position.y = 0;
        transform.position = position;

        gameObjects = flockPerception.GetGameObjects();
        if (gameObjects.Length > 0)
        {
            movement.ApplyForce(Steering.Cohesion(this, gameObjects));
            movement.ApplyForce(Steering.Separation(this, gameObjects, data.separationRadius) * data.separationWeight);
            movement.ApplyForce(Steering.Aligment(this, gameObjects) * data.alignmentWeight);
        }


        if (movement.acceleration.sqrMagnitude <= movement.maxForce * 0.1f)
        {
            movement.ApplyForce(Steering.Wander(this));
        }
    }
}