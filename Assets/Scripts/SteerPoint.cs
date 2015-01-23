using UnityEngine;
using System.Collections;

public class SteerPoint : MonoBehaviour
{
    public static bool GIZMO_VISUALIZE = true;
    public static float GIZMO_SIZE = 0.1f;

    [SerializeField]
    Transform _steerDirection;

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

    void OnDrawGizmos()
    {
        if (GIZMO_VISUALIZE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, GIZMO_SIZE);

            if (_steerDirection != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_steerDirection.transform.position, GIZMO_SIZE);
            }
        }
    }
}
