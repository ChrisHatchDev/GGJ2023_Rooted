using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Base Target;
    private int attack_damage = 3;
    public int Health = 100;
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
        if(inRange)
        {
            Target.Damage(this.attack_damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            Debug.Log("ENEMY NEAR BASE");
            Target.Damage(this.attack_damage);
            inRange = true;
        }

        if (other.tag == "TowerDamageRange")
        {
            Tower _tower = other.gameObject.GetComponentInParent<Tower>();
            _tower.AddEnemeyInRange(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            //Debug.Log("ENEMY NOT NEAR BASE");
            inRange = false;
        }

        if (other.tag == "TowerDamageRange")
        {
            Tower _tower = other.gameObject.GetComponentInParent<Tower>();
            _tower.RemoveEnemeyInRange(this);
        }
    }
    public void Damage(int damage)
    {
        Debug.Log("Enemy Taking Damage");
        Health -= damage;
    }
}
