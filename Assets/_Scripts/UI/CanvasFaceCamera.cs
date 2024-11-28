using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    //This script is responsible for making the loading bar face the camera

    //variable
    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main; //populating variable with the Camera from the scene
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        //changing the position of the gameobject which is the slider so that it always faces the camera
    }
}
