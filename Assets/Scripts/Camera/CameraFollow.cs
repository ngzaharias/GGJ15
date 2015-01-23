using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject _objectToFollow = null;
    public AnimationCurve _followCurve = AnimationCurve.Linear(0, 1, 10, 10);

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _objectToFollow.transform.position;
        float distance = Vector3.Magnitude(targetPos - currentPos);

        distance = _followCurve.Evaluate(distance);
        transform.position = Vector3.MoveTowards(currentPos, targetPos, distance * Time.fixedDeltaTime);
    }
}
