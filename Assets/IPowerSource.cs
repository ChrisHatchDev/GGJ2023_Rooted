using UnityEngine;

public enum PowerSourceType
{
    Base,
    Tower
}

public abstract class IPowerSource : MonoBehaviour
{
    public PowerSourceType PowerType;
    public abstract PowerSourceType GetPowerType();
}
