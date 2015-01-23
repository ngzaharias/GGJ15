using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform _objectToFollow = null;
    public AnimationCurve _followCurve = AnimationCurve.Linear(0, 1, 10, 10);

    public float _zoomSpeed = 100.0f;
    public float _zoomDistance = 5.0f;

    private Transform _camera = null;
    private float _scrollDelta = 0.0f;
    private const float _scrollDeltaFalloff = 0.2f;

    void Awake()
    {
        _camera = GetComponentInChildren<Camera>().transform;
    }

    void FixedUpdate()
    {
        _scrollDelta -= _scrollDelta * _scrollDeltaFalloff;
        _scrollDelta += Input.GetAxis("Mouse ScrollWheel");

        TargetFollow();
        TargetZoom(_scrollDelta);
    }

    void TargetFollow()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _objectToFollow.position;
        float distance = Vector3.Distance(currentPos, targetPos);

        distance = _followCurve.Evaluate(distance);
        transform.position = Vector3.MoveTowards(currentPos, targetPos, distance * Time.fixedDeltaTime);
    }

    void TargetZoom(float delta)
    {
        float zoomDelta = delta * _zoomSpeed;
        Vector3 targetPos = _camera.transform.localPosition + (_camera.forward * _zoomDistance);

         _camera.position = Vector3.MoveTowards(_camera.position, targetPos, zoomDelta * Time.fixedDeltaTime);
    }
}
