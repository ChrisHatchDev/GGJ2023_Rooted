using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Base Target;

    void Start()
    {
        Agent.SetDestination(Target.transform.position);
    }

    public void CheckRangeToBase()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            Debug.Log("ENEMY NEAR BASE");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            Debug.Log("ENEMY NOT NEAR BASE");
        }
    }


}
