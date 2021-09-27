using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{

    void OnTriggerEnter(Collider other) 
    {
        //this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
