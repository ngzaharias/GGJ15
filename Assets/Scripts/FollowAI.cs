using UnityEngine;
using System.Collections;

public class FollowAI : MonoBehaviour
{
    [SerializeField]
    Transform _followTarget = null;
    Transform _lookAtTarget = null;

    [SerializeField]
    float _force = 1.0f;

    [SerializeField]
    float _rotateSmoothTime = 1.0f;
    [SerializeField]
    float _rotateMaxSpeed = 1.0f;

    void Start()
    {
        _lookAtTarget = (new GameObject("LookAtTarget")).transform;
        _lookAtTarget.parent = transform;
        _lookAtTarget.position = transform.position + Vector3.forward;
    }

    void FixedUpdate()
    {
        if (_followTarget != null)
        {
            FollowTarget();
            LookAtTarget();
        }
    }

    void FollowTarget()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _followTarget.position;

        rigidbody.AddForce((targetPos - currentPos) * _force);
    }

    void LookAtTarget()
    {
        Vector3 currentLookAtPos = _lookAtTarget.position;
        Vector3 targetLookAtPos = _followTarget.position;
        Vector3 currentVelocity = new Vector3();

        targetLookAtPos = Vector3.SmoothDamp(currentLookAtPos, targetLookAtPos, ref currentVelocity, _rotateSmoothTime, _rotateMaxSpeed, Time.deltaTime);

        transform.LookAt(targetLookAtPos);
        _lookAtTarget.position = targetLookAtPos;
    }
}
