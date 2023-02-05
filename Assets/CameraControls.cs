using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform Pivot;
    public float RotateSpeed;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Transform camTransform = Camera.main.transform;

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
