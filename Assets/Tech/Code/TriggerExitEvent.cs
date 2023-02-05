using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerExitEvent : MonoBehaviour
{
    public string TagToCheck;
    public UnityEvent<Collider> OnEnter;
    public UnityEvent<Collider> OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagToCheck && other.transform.parent != this.transform.parent)
        {
            OnEnter.Invoke(other);
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagToCheck && other.transform.parent != this.transform.parent)
        {
            OnExit.Invoke(other);
        }
    }
}
