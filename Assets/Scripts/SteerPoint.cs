using UnityEngine;
using System.Collections;

public class SteerPoint : MonoBehaviour
{
    public static bool GIZMO_VISUALIZE = true;
    public static float GIZMO_SIZE = 0.1f;

    [SerializeField]
    Transform _steerDirection = null;

    [SerializeField]
    string _horizontalInputAxis = "Horizontal";
    [SerializeField]
    string _verticalInputAxis = "Vertical";

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

    [SerializeField]
    float _force = 10.0f;

    Rigidbody _parentRigidbody = null;

    public Vector3 Direction
    {
        get
        {
            if (_steerDirection != null)
            {
                return (_steerDirection.position - transform.position).normalized;
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
        _input.x = Input.GetAxis(_horizontalInputAxis);
        _input.y = Input.GetAxis(_verticalInputAxis);

        _lastAxisAngle = _axisAngle;
        _axisAngle = AxisAngle(_invertAxisAngle, _input);
        _axisAngleDelta = _axisAngle - _lastAxisAngle;

        if ((_axisAngle >= _minAxisAngleThreshold && _axisAngle < _maxAxisAngleThreshold) &&
            ((_rotateClockwise && _axisAngleDelta > 0.0f) || (!_rotateClockwise && _axisAngleDelta < 0.0f)))
        {
            _parentRigidbody.AddForceAtPosition(Direction * _force, transform.position);
        }
    }

    void OnDrawGizmos()
    {
        if (GIZMO_VISUALIZE)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, GIZMO_SIZE);

            if (_steerDirection != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_steerDirection.transform.position, GIZMO_SIZE);
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
