using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraMove : MonoBehaviour
{
 
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;
 
    [SerializeField] private Transform _lookAt;
    [SerializeField] private float _distance = 10.0f;
    private float _currentX = 0.0f;
     private float _currentY = 0.0f;
    public float _sensivity ;
 

    void Start()
    {

 
    }
    void LateUpdate()
    {
 
        _currentX += Input.GetAxis("Mouse X") * _sensivity * Time.deltaTime;
        _currentY += Input.GetAxis("Mouse Y") * _sensivity * Time.deltaTime;
 
        _currentY = Mathf.Clamp(_currentY, YMin, YMax);
 
        Vector3 Direction = new Vector3(0, 0, -_distance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        transform.position = _lookAt.position + rotation * Direction;
 
        transform.LookAt(_lookAt.position);
    }
}