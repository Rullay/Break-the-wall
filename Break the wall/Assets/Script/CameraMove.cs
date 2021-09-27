using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject caracter;
    private float cameraDistance;


    void Start()
    {
        cameraDistance = 40f;
    }


    void Update()
    {
        transform.position = new Vector3(caracter.transform.position.x, transform.position.y, caracter.transform.position.z - cameraDistance);
    }

    public void Distancing()
    {
        cameraDistance += 0.2f;
        transform.position = new Vector3(caracter.transform.position.x, transform.position.y + 0.15f, transform.position.y - cameraDistance);
    }

    public void CameraZoom()
    {
        cameraDistance -= 0.2f;
        transform.position = new Vector3(caracter.transform.position.x, transform.position.y - 0.15f, transform.position.y - cameraDistance);
    }
}
