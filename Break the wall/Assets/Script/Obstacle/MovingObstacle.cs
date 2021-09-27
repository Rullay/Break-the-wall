using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : Obstacle
{
    public float obstacleSpeed;
    private float boundaries = 14f;
    public float move_x;

    void Start()
    {
        move_x = 1;
    }

  
     public override void  Update()
     {
        base.Update();
     }

    public override void Move()
    {   
        if(transform.position.x > boundaries)
        {
            move_x = 1;
        }     
        if(transform.position.x < -boundaries)
        {
            move_x = -1;
        }
        transform.Translate(new Vector3(move_x, 0, 0) * obstacleSpeed * Time.deltaTime);
    }


}
