using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle : Obstacle
{
    public float frequency;



    public override void Move()
    {
      transform.Rotate(new Vector3(0, 360, 0) * frequency * Time.deltaTime);
    }
}
