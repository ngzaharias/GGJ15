using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    SteerPoint _leftSteerPoint;
    [SerializeField]
    SteerPoint _rightSteerPoint;

    [SerializeField]
    float _force = 10;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidbody.AddForceAtPosition(_leftSteerPoint.Direction * _force, _leftSteerPoint.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidbody.AddForceAtPosition(_rightSteerPoint.Direction * _force, _rightSteerPoint.transform.position);
        }
    }
}
