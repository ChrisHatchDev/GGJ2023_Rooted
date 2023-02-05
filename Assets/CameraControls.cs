using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform Pivot;
    public float RotateSpeed = 50;
    public float ZoomSpeed = 20;

    Transform camTransform;

    private void Start()
    {
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector2 scrollDelta = Input.mouseScrollDelta;
        if (scrollDelta.magnitude > 0.05)
        {
            Camera.main.transform.position += camTransform.forward * (scrollDelta.y * ZoomSpeed * Time.deltaTime);
        }

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            camTransform.RotateAround(Pivot.transform.position, Vector3.up, (RotateSpeed * -horizontal) * Time.deltaTime);
        }

        float vertical = Input.GetAxis("Vertical");

        if (vertical != 0)
        {
            camTransform.RotateAround(Pivot.transform.position, transform.right, (RotateSpeed * vertical) * Time.deltaTime);
        }

        // Vector3 eulerAngles = camTransform.eulerAngles;

        // camTransform.rotation = Quaternion.Euler(new Vector3(
        //     ClampAngle(eulerAngles.x, -60f, 60f),
        //     eulerAngles.y,
        //     ClampAngle(eulerAngles.z, -60f, 60f)
        // ));
    }

    float ClampAngle(float angle, float from, float to)
     {
         // accepts e.g. -80, 80
         if (angle < 0f) angle = 360 + angle;
         if (angle > 180f) return Mathf.Max(angle, 360+from);
         return Mathf.Min(angle, to);
     }
}
