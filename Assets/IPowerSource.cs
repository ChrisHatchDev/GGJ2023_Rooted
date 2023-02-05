using UnityEngine;

public enum PowerSourceType
{
    Base,
    Tower
}

public abstract class IPowerSource : MonoBehaviour
{
    public bool HasPower;
    public PowerSourceType PowerType;
    public abstract PowerSourceType GetPowerType();
}
