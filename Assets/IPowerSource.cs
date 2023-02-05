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
    public List<IPowerSource> PowerSourceList = new List<IPowerSource>();
    public IPowerSource PowerSource;
    public Transform LineConnectionPoint;
    public PowerSourceType PowerType;
    public abstract PowerSourceType GetPowerType();
}
