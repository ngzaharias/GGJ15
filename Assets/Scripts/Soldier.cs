using UnityEngine;
using System.Collections;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    string _inputPrefix;
    [SerializeField]
    Transform _toe;
    [SerializeField]
    Transform _hip;
    [SerializeField]
    Transform _vesselRoot;
    [SerializeField]
    Transform _oar;
    [SerializeField]
    Transform _oarAnchorPoint;
    [SerializeField]
    Vector3 _oarLookAtUpVector;

    Vector3 _rootToeInitialVector;

    Vector3 _toeInitialOffset;
    Vector3 _soldierInitialPosition;

    void Start()
    {
        _rootToeInitialVector = transform.position - _toe.position;
        //_rootToeInitialVector = transform.localPosition - _toe.position;
        _soldierInitialPosition = transform.localPosition;
    }

    void Update()
    {
        Vector2 analogStick = new Vector2(Input.GetAxis(_inputPrefix + "_XAxis_1"), Input.GetAxis(_inputPrefix + "_YAxis_1"));
        float angle = AxisAngle(_inputPrefix == "L" ? true : false, analogStick);
        //float time = 84.0f * (angle / 360.0f);
        float time = 1.8f * (angle / 360.0f) + (_inputPrefix == "L" ? 0.0f : 0.9f);
        GetComponent<Animation>()["Take 001"].time = analogStick == Vector2.zero ? 0.0f : time;
    }

    void LateUpdate()
    {
        //// Offset position to root the feet
        //Vector3 rootToeVector = transform.position - _toe.position;
        ////Vector3 rootToeVector = transform.localPosition - _toe.position;
        //Vector3 offset = _vesselRoot.TransformDirection(rootToeVector - _rootToeInitialVector);
        //transform.localPosition = _soldierInitialPosition + offset;
        if (_oar != null)
        {
            _oar.transform.LookAt(_oarAnchorPoint, _oarLookAtUpVector);
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
