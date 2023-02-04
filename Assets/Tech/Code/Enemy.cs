using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Base Target;
    private int attack_damage = 3;
    private int Health = 100;
    private bool inRange = false;

    void Start(){
        Agent.SetDestination(Target.transform.position);
    }
    void Update(){
        if(this.Health <= 0){
            Destroy(gameObject);
        }
    }
    public void CheckRangeToBase()
    {

    }

    private void OnTriggerStay(Collider other){
        if(inRange){
            Target.Damage(this.attack_damage);
            this.Damage(Target.attack_damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            Debug.Log("ENEMY NEAR BASE");
            Target.Damage(this.attack_damage);
            this.Damage(Target.attack_damage);
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            //Debug.Log("ENEMY NOT NEAR BASE");
            inRange = false;
        }
    }
    public void Damage(int damage){
        Health -= damage;
    }
}
