using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionObstacle : Obstacle
{
    private float BaseScale = 1;
    private float BigScale = 7;
    private bool ischange;
    public float changePerSecond;

    public override void Update()
    {
        base.Update();
    }

    public override void Move()
    {
        if(transform.localScale.x < BigScale && ischange == false)
        {
            transform.localScale += new Vector3(changePerSecond, 0, changePerSecond) * Time.deltaTime;
            if(transform.localScale.x >= BigScale)
            {
                ischange = true;
            }
        }
        
        if(transform.localScale.x > BaseScale && ischange == true)
        {
            transform.localScale -= new Vector3(changePerSecond, 0, changePerSecond) * Time.deltaTime;
            if (transform.localScale.x <= BaseScale)
            {
                ischange = false;
            }
        }
      
    }
}
