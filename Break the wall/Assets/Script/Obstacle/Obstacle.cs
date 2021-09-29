using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public int decrease = 4;


    public void Update()
    {
        Move();
    }


    public abstract void Move();
    
    
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
