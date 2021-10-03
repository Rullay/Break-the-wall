using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cxv : MonoBehaviour
{

    void Awake()
    {
        Debug.Log("Awake");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

   

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }
}
