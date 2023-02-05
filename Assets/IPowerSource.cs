using System.Collections.Generic;
using UnityEngine;

public enum PowerSourceType
{
    Base,
    Tower
}

public abstract class IPowerSource : MonoBehaviour
{
    public bool HasPower;
    public HashSet<IPowerSource> PowerSourceList = new HashSet<IPowerSource>();
    public Transform LineConnectionPoint;
    public PowerSourceType PowerType;
    public abstract PowerSourceType GetPowerType();
}
