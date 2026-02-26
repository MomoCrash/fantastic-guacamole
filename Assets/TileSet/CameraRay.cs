using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRay : MonoBehaviour
{

    public Vector3 RayCastFromCamera()
    {
        var hits = Physics.RaycastAll(transform.position, transform.forward);
        if (hits.Length == 0) return Vector3.zero;
        return hits[0].point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100.0f);
    }

    private void FixedUpdate()
    {
        

    }
}
