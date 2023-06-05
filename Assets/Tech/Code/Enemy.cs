using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Base Target;
    public int attack_damage = 3;
    public float Health = 100;
    private float MaxHealth = 100;
    private bool inRange = false;

    public Image HealthBarFillImage;

    void Start()
    {
        MaxHealth = Health;

        if (Target)
        {
            Agent.SetDestination(Target.transform.position);
            Agent.speed += Random.Range((float)-0.12, (float)0.12);
        }

        StartCoroutine(DamageCycle());
    }
    void Update(){
        if(this.Health <= 0){
            EnemySpawner.NumberOfEnemiesKilled += 1;
            Destroy(gameObject);
        }

        HealthBarFillImage.fillAmount = (Health / MaxHealth);
    }
    public void CheckRangeToBase()
    {

    }

    private IEnumerator DamageCycle()
    {
        yield return new WaitForSeconds(0.5f);

        if(inRange)
        {
            Target.Damage(this.attack_damage);
        }
        
        StartCoroutine(DamageCycle());
    }

    // private void OnTriggerStay(Collider other){
    //     if(inRange)
    //     {
    //         Target.Damage(this.attack_damage);
    //     }
    // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BaseDamageRange")
        {
            // Debug.Log("ENEMY NEAR BASE");
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
        // Debug.Log("Enemy Taking Damage");
        Health -= damage;
    }
}
