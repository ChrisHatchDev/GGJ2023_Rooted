using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Base Target;
    private int attack_damage = 3;
    private int Health = 100;

    void Start(){
        Agent.SetDestination(Target.transform.position);
    }
    void Update(){
        if(this.Health <= 0){
            Destroy(this);
        }
    }
    public void CheckRangeToBase()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            //Debug.Log("ENEMY NEAR BASE");
            Target.Damage(this.attack_damage);
            this.Damage(Target.attack_damage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            //Debug.Log("ENEMY NOT NEAR BASE");
        }
    }
    public void Damage(int damage){
        Health -= damage;
    }
}
