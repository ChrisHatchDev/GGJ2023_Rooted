using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLineController : MonoBehaviour
{
    public LineRenderer LineComponent;

    public void SetLinePoints(Vector3 start, Vector3 end)
    {
        LineComponent.SetPositions(new Vector3[]{start, end});
    }
}
