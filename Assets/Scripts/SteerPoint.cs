using UnityEngine;
using System.Collections;

public class SteerPoint : MonoBehaviour
{
    //[System.Serializable]
    //public class SteerDirection
    //{
    //    [SerializeField]
    //    Transform _direction;
    //    [SerializeField]
    //    float _force;

    //    public Vector3 Direction(Vector3 position)
    //    {
    //        if (_direction != null)
    //        {
    //            return (_direction.position - position).normalized * _force;
    //        }

    //        return Vector3.zero;
    //    }

    //    public Vector3 Position
    //    {
    //        get
    //        {
    //            if (_direction != null)
    //            {
    //                return _direction.position;
    //            }

    //            return Vector3.zero;
    //        }
    //    }
    //}

    public static bool GIZMO_VISUALIZE = true;
    public static float GIZMO_SIZE = 0.1f;

    [SerializeField]
    //SteerDirection[] _steerDirections;
    Transform _steerDirection = null;

    [SerializeField]
    float _force = 50.0f;

    [SerializeField]
    string[] _horizontalInputAxes = { "L_XAxis_1", "L_Horizontal" };
    [SerializeField]
    string[] _verticalInputAxes = { "L_YAxis_1", "L_Vertical" };

    Vector2 _input = new Vector2();

    float _axisAngle = 0.0f;
    float _lastAxisAngle = 0.0f;
    float _axisAngleDelta = 0.0f;

    [SerializeField]
    bool _invertAxisAngle = false;

    [SerializeField]
    bool _rotateClockwise = true;

    [SerializeField]
    float _minAxisAngleThreshold = 90.0f;
    [SerializeField]
    float _maxAxisAngleThreshold = 270.0f;

    Rigidbody _parentRigidbody = null;

    public Vector3 Direction
    {
        get
        {
            if (_steerDirection != null)
            {
                return (_steerDirection.position - transform.position).normalized * _force;
            }

            return Vector3.zero;
        }
    }

    void Start()
    {
        _parentRigidbody = transform.parent.rigidbody;
    }

    void Update()
    {
        _input = Vector2.zero;

        foreach (string horizontalInputAxis in _horizontalInputAxes)
        {
            _input.x += Input.GetAxis(horizontalInputAxis);
        }

        foreach (string verticalInputAxis in _verticalInputAxes)
        {
            _input.y += Input.GetAxis(verticalInputAxis);
        }

        _lastAxisAngle = _axisAngle;
        _axisAngle = AxisAngle(_invertAxisAngle, _input);
        _axisAngleDelta = _axisAngle - _lastAxisAngle;

        if ((_axisAngle >= _minAxisAngleThreshold && _axisAngle < _maxAxisAngleThreshold) &&
            ((_rotateClockwise && _axisAngleDelta > 0.0f) || (!_rotateClockwise && _axisAngleDelta < 0.0f)))
        {
            //foreach (SteerDirection steerDirection in _steerDirections)
            //{
            //    _parentRigidbody.AddForceAtPosition(steerDirection.Direction(transform.position), transform.position);
            //}

            _parentRigidbody.AddForceAtPosition(Direction, transform.position);
        }
    }

    void OnDrawGizmos()
    {
        if (GIZMO_VISUALIZE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, GIZMO_SIZE);

            //foreach (SteerDirection steerDirection in _steerDirections)
            //{
            //    Gizmos.color = Color.yellow;
            //    Gizmos.DrawSphere(steerDirection.Position, GIZMO_SIZE);
            //}

            if (_steerDirection != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_steerDirection.position, GIZMO_SIZE);
            }
        }
    }

    float AxisAngle(bool invert, Vector2 input)
    {
        if (input != Vector2.zero)
        {
            float verticalAxisAngle = Vector2.Angle((invert ? -Vector2.up : Vector2.up), input);
            float horizontalAxisAngle = Vector2.Angle(Vector2.right, input);

            if (horizontalAxisAngle > 90.0f)
            {
                return 360.0f - verticalAxisAngle;
            }

            return verticalAxisAngle;
        }

        return 0.0f;
    }
}
