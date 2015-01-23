using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    SteerPoint _leftSteerPoint = null;
    [SerializeField]
    SteerPoint _rightSteerPoint = null;

    [SerializeField]
    float _force = 10.0f;

    //void Update()
    //{
    //    if (Input.GetAxis("Horizontal") < 0)
    //    {
    //        rigidbody.AddForceAtPosition(_leftSteerPoint.Direction * _force, _leftSteerPoint.transform.position);
    //    }

    //    if (Input.GetAxis("Horizontal") > 0)
    //    {
    //        rigidbody.AddForceAtPosition(_rightSteerPoint.Direction * _force, _rightSteerPoint.transform.position);
    //    }
    //}
}
