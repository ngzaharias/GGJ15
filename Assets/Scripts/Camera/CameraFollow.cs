using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private const float _scrollDeltaFalloff = 0.05f;

    public Transform _target = null;
    public AnimationCurve _followCurve = AnimationCurve.Linear(0, 1, 10, 10);

    public float _zoomSpeed = 50.0f;
    public float _zoomMinDistance = 5.0f;
    public float _zoomMaxDistance = 15.0f;

    public float _rotateYawSpeed = 50.0f;
    //public float _rotateRollSpeed = 10.0f;
    //public float _rotateRollLimit = 45.0f;

    private Transform _camera = null;
    private float _scrollDelta = -5.0f;
    private float _zoomDelta = 0.0f;

    void Awake()
    {
        _camera = GetComponentInChildren<Camera>().transform;
    }

    void FixedUpdate()
    {
        _scrollDelta -= _scrollDelta * _scrollDeltaFalloff;
        _scrollDelta += Input.GetAxis("Mouse ScrollWheel");

        TargetFollow();
        TargetZoom();
		TargetRotate();
    }

    void TargetFollow()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _target.position + new Vector3(0, 2, 0);
        float distance = Vector3.Distance(currentPos, targetPos);

        distance = _followCurve.Evaluate(distance);
        transform.position = Vector3.MoveTowards(currentPos, targetPos, distance * Time.fixedDeltaTime);
    }

    void TargetZoom()
    {
        Vector3 targetPos = _camera.position;

        _zoomDelta = _scrollDelta * _zoomSpeed;

        if (_zoomDelta < 0)
        {
            targetPos = _camera.position - (_camera.forward * _zoomMinDistance);
        }
        else if (_zoomDelta > 0)
        {
            targetPos = _camera.position + (_camera.forward * _zoomMaxDistance);
        }

        _zoomDelta = Mathf.Abs(_zoomDelta);
        _camera.position = Vector3.MoveTowards(_camera.position, targetPos, _zoomDelta * Time.fixedDeltaTime);

        //  correction to stop exceeding zoom levels
        float distance = Vector3.Distance(transform.position, _camera.position);
        if (distance < _zoomMinDistance)
        {
            targetPos = transform.position + (-_camera.forward * _zoomMinDistance);
            _camera.position = Vector3.MoveTowards(_camera.position, targetPos, _zoomSpeed * Time.fixedDeltaTime);
            _scrollDelta = 0.0f;
        }
        if (distance > _zoomMaxDistance)
        {
            targetPos = transform.position + (-_camera.forward * _zoomMaxDistance);
            _camera.position = Vector3.MoveTowards(_camera.position, targetPos, _zoomSpeed * Time.fixedDeltaTime);
            _scrollDelta = 0.0f;
        }
    }

    void TargetRotate()
    {
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = _target.rotation;
        Quaternion newRot = currentRot;

        Quaternion yawRot = targetRot;
        yawRot.x = 0;
        yawRot.z = 0;

        Quaternion rollRot = targetRot;
        rollRot.x = 0;
        rollRot.y = 0;

        transform.rotation = Quaternion.RotateTowards(currentRot, yawRot, _rotateYawSpeed * Time.fixedDeltaTime);

        //// if we've rolled past a limit, go back to a normal roll
        //if (targetRot.eulerAngles.z > _rotateRollLimit && targetRot.eulerAngles.z < 360 - _rotateRollLimit)
        //{
        //    rollRot = currentRot;
        //    rollRot.z = 0;
        //}

        //newRot = newRot * Quaternion.RotateTowards(currentRot, yawRot, _rotateYawSpeed * Time.fixedDeltaTime) * Quaternion.Inverse(currentRot);
        //newRot = newRot * (Quaternion.RotateTowards(currentRot, rollRot, _rotateRollSpeed * Time.fixedDeltaTime) * Quaternion.Inverse(currentRot));
        //transform.rotation = newRot;
    }
}
